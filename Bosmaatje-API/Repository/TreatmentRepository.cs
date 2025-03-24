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
            var result = await sqlConnection.QueryAsync<TreatmentReadDto>(
                "SELECT t.TreatmentId, t.[Name] AS 'treatmentName', t.ImagePath, t.VideoPath, t.[Date], t.[Order], t.DoctorName, t.TreatmentPlanName, t.IsCompleted, t.StickerId FROM [Configuration] c LEFT JOIN Treatment t ON c.TreatmentPlanName = t.TreatmentPlanName WHERE c.TreatmentPlanName = @treatmentPlanName AND c.Email = @email ORDER BY t.[Order]",
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
            var result = await sqlConnection.QueryAsync<string>(
                "SELECT Content FROM [Description] WHERE TreatmentId = @treatmentId ORDER BY [Order]",
                new
                {
                    treatmentId
                });
            return result.ToList();
        }

        public async Task Update(TreatmentUpdateDto treatmentUpdateDto, Guid treatmentId, string email)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            var result = await sqlConnection.QuerySingleOrDefaultAsync<TreatmentReadDto>(
                "SELECT * FROM [Treatment] t LEFT JOIN [Configuration] c ON t.TreatmentPlanName = c.TreatmentPlanName WHERE t.TreatmentId = @treatmentId AND c.Email = @email",
                new
                {
                    treatmentId, email
                });
            
            if (result == null) return;

            await sqlConnection.ExecuteAsync(
                "UPDATE [Treatment] SET [Date] = @date, DoctorName = @doctorName, StickerId = @stickerId WHERE TreatmentId = @treatmentId",
                new
                {
                    treatmentId,
                    date = treatmentUpdateDto.date ?? result!.date,
                    doctorName = treatmentUpdateDto.doctorName ?? result!.doctorName,
                    stickerId = treatmentUpdateDto.stickerId ?? result!.stickerId
                });
        }
    }
}