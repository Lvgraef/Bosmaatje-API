using Bosmaatje_API.Dto;

namespace Bosmaatje_API.IRepository;

public interface IConfigurationRepository
{
    Task Create(ConfigurationCreateDto configurationCreateDto, string email);
    Task<bool> ConflictCheck(string email);
    Task<ConfigurationReadDto?> Read(string email);
    Task Update(ConfigurationUpdateDto configurationUpdateDto, string email);
    Task Delete(string email);
}