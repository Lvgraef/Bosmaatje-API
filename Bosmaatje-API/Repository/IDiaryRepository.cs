using Bosmaatje_API.Dto;

namespace Bosmaatje_API.Repository;

public interface IDiaryRepository
{
    Task Create(DiaryCreateDto diaryCreateDto, string email);
    Task<List<DiaryReadDto>> Read(string email, DateTime? date);
    Task Update(DiaryUpdateDto diaryUpdateDto, string email, DateTime date);

    Task Delete(string email, DateTime date);

}