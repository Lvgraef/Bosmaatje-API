using Bosmaatje_API.Dto;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Repository
{
    public class AppointmentRepository(string sqlConnectionString) : IAppointmentRepository
    {
        public async Task Create(AppointmentCreateDto appointmentCreateDto, string email)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            await sqlConnection.ExecuteAsync("INSERT INTO [Appointment] ([Name], Email, [Date]) VALUES (@name, @email, @date)",
                new
                {
                    appointmentCreateDto.name, email, appointmentCreateDto.date
                });
        }

        public async Task<List<AppointmentReadDto>> Read(string email)
        {
            List<AppointmentReadDto> appointments = new List<AppointmentReadDto>();
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            var result = await sqlConnection.QueryAsync<AppointmentReadDto>("SELECT * FROM [Appointment] WHERE Email = @email", 
                new
                {
                    email
                });
            foreach (var appointment in result)
            {
                appointments.Add(appointment);
            }
            return appointments;
        }

        public async Task Delete(Guid appointmentId)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            await sqlConnection.ExecuteAsync("DELETE FROM [Appointment] WHERE AppointmentId = @appointmentId", 
                new
                {
                    appointmentId
                });
        }
    }
}

