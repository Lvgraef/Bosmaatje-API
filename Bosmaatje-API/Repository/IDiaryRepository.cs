using Bosmaatje_API.Dto;

namespace Bosmaatje_API.Repository;

public interface IDiaryRepository
{
    Task Create(DiaryCreateDto diaryCreateDto, string email);
    Task<DiaryReadDto?> Read(string email);
    Task Update(DiaryUpdateDto diaryUpdateDto, string email);
    Task Delete(string email);
}