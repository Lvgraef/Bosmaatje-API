using Bosmaatje_API.Dto;
using Bosmaatje_API.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Repository
{
    public class TreatmentRepository(string sqlConnectionString) : ITreatmentRepository
    {
        public async Task<TreatmentReadDto?> Read(string email)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            var result = await sqlConnection.QueryFirstOrDefaultAsync(" SELECT t.TreatmentId, t.[Name], t.ImageUri, t.[Date], t.[Order], t.DoctorName, t.TreatmentPlanName FROM [Configuration] c\nLEFT JOIN Treatment t ON c.TreatmentPlanName = t.TreatmentPlanName WHERE c.Email = @email");
            return result;
        }

        public async Task Update(TreatmentUpdateDto treatmentUpdateDto, Guid treatmentId)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            await sqlConnection.ExecuteAsync("UPDATE [Treatment] SET Date = @date, DoctorName = @doctorName WHERE TreatmentId = @treatmentId", 
                new
                {
                    treatmentUpdateDto.date, treatmentUpdateDto.doctorName
                });
        }
    }
}


