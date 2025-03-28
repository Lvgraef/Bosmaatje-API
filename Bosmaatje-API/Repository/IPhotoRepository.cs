using Bosmaatje_API.Dto;

namespace Bosmaatje_API.Repository;

public interface IPhotoRepository
{
    Task Create(PhotoCreateDto photoCreateDto);
    Task<PhotoReadDto> Read(string email);
    Task Delete(string email);
}