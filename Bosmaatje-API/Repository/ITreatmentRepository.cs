using Bosmaatje_API.Dto;

namespace Bosmaatje_API.IRepository;

public interface ITreatmentRepository
{
    Task<TreatmentReadDto?> Read(string email);
    Task Update(TreatmentUpdateDto treatmentUpdateDto, Guid treatmentId);
}