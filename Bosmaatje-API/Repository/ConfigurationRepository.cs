using Bosmaatje_API.Dto;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Repository
{
    public class ConfigurationRepository(string sqlConnectionString) : IConfigurationRepository
    {
        public async Task Create(ConfigurationCreateDto configurationCreateDto, string email)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            await sqlConnection.ExecuteAsync(
                "INSERT INTO [Configuration] (ChildName, ChildBirthDate, PrimaryDoctorName, CharacterId, Email, TreatmentPlanName) VALUES (@childName, @childBirthDate, @primaryDoctorName, @characterId, @email, @treatmentPlanName)",
                new
                {
                    email, configurationCreateDto.childName, configurationCreateDto.childBirthDate,
                    configurationCreateDto.primaryDoctorName, configurationCreateDto.characterId,
                    configurationCreateDto.treatmentPlanName
                });

            IEnumerable<Guid> treatments = await sqlConnection.QueryAsync<Guid>(
                "SELECT TreatmentId FROM Treatment WHERE TreatmentPlanName = @treatmentPlanName",
                new
                {
                    configurationCreateDto.treatmentPlanName
                });

            foreach (var treatment in treatments)
            {
                await sqlConnection.ExecuteAsync(
                    "INSERT INTO [TreatmentInfo] (Email, [Date], DoctorName, StickerId, TreatmentId) VALUES (@email, null, @doctor, null, @id)",
                    new
                    {
                        email, doctor = configurationCreateDto.primaryDoctorName, id = treatment
                    });
            }
        }

        public async Task<bool> ConflictCheck(string email)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            var result = await sqlConnection.QueryAsync<string>("SELECT Email FROM [Configuration]");

            foreach (var item in result)
            {
                if (item == email)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<ConfigurationReadDto?> Read(string email)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            var result = await sqlConnection.QuerySingleOrDefaultAsync<ConfigurationReadDto>(
                " SELECT * FROM [Configuration] WHERE Email = @email",
                new
                {
                    email
                });
            return result;
        }

        public async Task Update(ConfigurationUpdateDto configurationUpdateDto, string email)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            await sqlConnection.ExecuteAsync(
                "UPDATE [Configuration] SET PrimaryDoctorName = @primaryDoctorName, CharacterId = @characterId,  WHERE Email = @email",
                new
                {
                    email, configurationUpdateDto.primaryDoctorName, configurationUpdateDto.characterId
                });
        }

        public async Task Delete(string email)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            await sqlConnection.ExecuteAsync("DELETE FROM [Configuration] WHERE Email = @email",
                new
                {
                    email
                });
        }
    }
}