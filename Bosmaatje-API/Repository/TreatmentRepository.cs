using Bosmaatje_API.Dto;
using Bosmaatje_API.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Repository
{
    public class TreatmentRepository(string sqlConnectionString) : ITreatmentRepository
    {
        public async Task<List<TreatmentReadDto?>> Read(string email, string treatmentPlanName)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            var result = await sqlConnection.QueryAsync<TreatmentReadDto>(" SELECT t.TreatmentId, t.[Name], t.ImageUri, t.VideoUri t.[Date], t.[Order], t.DoctorName, t.TreatmentPlanName, t.Description, t.IsCompleted FROM [Configuration] cLEFT JOIN Treatment t ON c.TreatmentPlanName = t.TreatmentPlanName WHERE c.TreatmentPlanName = @treatmentPlanName AND c.Email = @email",
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

        public async Task<List<string>> GetDescription(Guid treatmentId)
        {
            List<string> description = new List<string>();
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            var result = await sqlConnection.QueryAsync<string>("SELECT Page FROM [Treatments] WHERE TreatmentId = @treatmentId ORDER BY Order",
               new
               {
                    treatmentId
                });
            foreach (var item in result)
            {
                description.Add(item);
            }
            return description;
        }

        public async Task Update(TreatmentUpdateDto treatmentUpdateDto, string treatmentPlanName)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            await sqlConnection.ExecuteAsync("UPDATE [Treatment] SET Date = @date, DoctorName = @doctorName WHERE TreatmentPlanName = @treatmentPlanName", 
                new
                {
                    treatmentPlanName, treatmentUpdateDto.date, treatmentUpdateDto.doctorName
                });
        }
    }
}


