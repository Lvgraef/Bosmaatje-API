using Dapper;
using Microsoft.Data.SqlClient;
using Dto;

namespace Repositories
{
    public class ConfigurationRepository
    {
        private readonly string _sqlConnectionString;

        public ConfigurationRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public async Task<bool> Create(ConfigurationCreateDto configurationCreateDto, string Email)
        {
            bool Succesfull = false;
            try
            {
                using (var SqlConnection = new SqlConnection(_sqlConnectionString))
                {
                    await SqlConnection.ExecuteAsync($"Insert Into [Configuration] (ChildName, ChildBirthDate, PrimaryDoctorName, CharacterId, Email, TreatmentPlanName) Values ('@ConfigurationCreateDto.childName', @ConfigurationCreateDto.childBirthDay, '@configurationCreateDto.primaryDoctorName', @configurationCreateDto.characterId, '@Email', '@configurationCreate.treatmentCreateDto')");
                }
            }

            catch(SqlException ex)
            {
                return Succesfull;
            }

            catch(Exception ex)
            {
                return Succesfull;
            }
            Succesfull = true;
            return Succesfull;
        }

        public async Task<ConfigurationReadDto> Read(string Email)
        {
            using(var SqlConnection = new SqlConnection(_sqlConnectionString))
            {
                var Result = await SqlConnection.QuerySingleOrDefaultAsync($" Select * From [Configuration] Where Email = '@Email'");
                return Result;
            }
        }

        public async Task<bool> Update(ConfigurationUpdateDto configurationUpdateDto, string Email)
        {
            bool Succesfull = false;
            try
            {
                using (var SqlConnection = new SqlConnection(_sqlConnectionString))
                {
                    await SqlConnection.ExecuteAsync($"Update [Configuration] Set (ChildName, ChildBirthDate, PrimaryDoctorName, CharacterId, Email, TreatmentPlanName) Where Email = '@Email' Values ('@ConfigurationCreateDto.childName', @ConfigurationCreateDto.childBirthDay, '@configurationCreateDto.primaryDoctorName', @configurationCreateDto.characterId, '@Email', '@configurationCreate.treatmentCreateDto')");
                }
            }

            catch (SqlException ex)
            {
                return Succesfull;
            }

            catch (Exception ex)
            {
                return Succesfull;
            }
            Succesfull = true;
            return Succesfull;
        }

        public async Task<bool> Delete(`string Email)
        {
            bool Succesfull = false;
            try
            {
                using (var SqlConnection = new SqlConnection(_sqlConnectionString))
                {
                    await SqlConnection.ExecuteAsync($"DELETE FROM [Configuration] WHERE Email = @Email");
                }
            }

            catch (SqlException ex)
            {
                return Succesfull;
            }

            catch (Exception ex)
            {
                return Succesfull;
            }
            Succesfull = true;
            return Succesfull;
        }
    }
}
