using Bosmaatje_API.Dto;
using Bosmaatje_API.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Repository
{
    public class ConfigurationRepository(string sqlConnectionString) : IConfigurationRepository
    {
        public async Task<bool> Create(ConfigurationCreateDto configurationCreateDto, string email)
        {
            var succesfull = false;
            try
            {
                await using var sqlConnection = new SqlConnection(sqlConnectionString);
                await sqlConnection.ExecuteAsync($"Insert Into [Configuration] (ChildName, ChildBirthDate, PrimaryDoctorName, CharacterId, Email, TreatmentPlanName) Values ('@ConfigurationCreateDto.childName', @ConfigurationCreateDto.childBirthDay, '@configurationCreateDto.primaryDoctorName', @configurationCreateDto.characterId, '@email', '@configurationCreate.treatmentCreateDto')");
            }
            catch(SqlException ex)
            {
                return succesfull;
            }
            succesfull = true;
            return succesfull;
        }
        public async Task<ConfigurationReadDto?> Read(string email)
        {
            await using var sqlConnection = new SqlConnection(sqlConnectionString);
            var result = await sqlConnection.QuerySingleOrDefaultAsync($" Select * From [Configuration] Where Email = '@Email'");
            return result;
        }
        public async Task<bool> Update(ConfigurationUpdateDto configurationUpdateDto, string email)
        {
            var successful = false;
            try
            {
                await using var sqlConnection = new SqlConnection(sqlConnectionString);
                await sqlConnection.ExecuteAsync("Update [Configuration] Set (ChildName, ChildBirthDate, PrimaryDoctorName, CharacterId, Email, TreatmentPlanName) Values ('@ConfigurationUpdateDto.childName', @ConfigurationUpdateDto.childBirthDay, '@configurationUpdateDto.primaryDoctorName', @configurationUpdateDto.characterId, '@email', '@configurationUpdateDto.treatmentPlanName') Where Email = '@email'");
            }
            catch (SqlException ex)
            {
                return successful;
            }
            successful = true;
            return successful;
        }

        public async Task<bool> Delete(string email)
        {
            var successfull = false;
            try
            {
                await using var sqlConnection = new SqlConnection(sqlConnectionString);
                await sqlConnection.ExecuteAsync($"DELETE FROM [Configuration] WHERE Email = @Email");
            }
            catch (SqlException ex)
            {
                return successfull;
            }
            successfull = true;
            return successfull;
        }
    }
}
