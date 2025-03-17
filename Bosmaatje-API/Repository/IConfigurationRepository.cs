using Bosmaatje_API.Dto;

namespace Bosmaatje_API.IRepository;

public interface IConfigurationRepository
{
    Task<bool> Create(ConfigurationCreateDto configurationCreateDto, string email);
    Task<ConfigurationReadDto?> Read(string email);
    Task<bool> Update(ConfigurationUpdateDto configurationUpdateDto, string email);
    Task<bool> Delete(string email);
}