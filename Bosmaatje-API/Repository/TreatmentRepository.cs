using Bosmaatje_API.Dto;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Repository
{
    public class TreatmentRepository(string sqlConnectionString) : ITreatmentRepository
    {
        public async Task<List<TreatmentReadDto?>> Read(string email, string treatmentPlanName)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            var result = await sqlConnection.QueryAsync<TreatmentReadDto>("SELECT t.TreatmentId, t.[Name], t.ImagePath, t.VideoPath, t.[Date], t.[Order], t.DoctorName, t.TreatmentPlanName, t.IsCompleted FROM [Configuration] c LEFT JOIN Treatment t ON c.TreatmentPlanName = t.TreatmentPlanName WHERE c.TreatmentPlanName = @treatmentPlanName AND c.Email = @email",
                new
                {
                    email, treatmentPlanName
                });
            foreach (var treatment in result)
            {
                treatment.description = await GetDescription(treatment.treatmentId);
            }
            return result.ToList();
        }

        private async Task<List<string>> GetDescription(Guid treatmentId)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            var result = await sqlConnection.QueryAsync<string>("SELECT Content FROM [Description] WHERE TreatmentId = @treatmentId ORDER BY [Order]",
               new
               {
                    treatmentId
                });
            return result.ToList();
        }

        public async Task Update(TreatmentUpdateDto treatmentUpdateDto, string treatmentId, string email, string treatmentPlanName)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            var result = (await Read(email, treatmentPlanName))[0];
            await sqlConnection.ExecuteAsync("UPDATE [Treatment] SET Date = @date, DoctorName = @doctorName WHERE TreatmentId = @treatmentId", 
                new
                {
                    treatmentId, date = treatmentUpdateDto.date ?? result!.date, doctorName = treatmentUpdateDto.doctorName ?? result!.doctorName
                });
        }
    }
}


