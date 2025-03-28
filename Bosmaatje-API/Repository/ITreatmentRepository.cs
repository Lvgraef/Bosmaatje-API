using Bosmaatje_API.Dto;

namespace Bosmaatje_API.Repository;

public interface ITreatmentRepository
{
    Task<List<TreatmentReadDto?>> Read(string email, string? treatmentPlanName);
    Task Update(TreatmentUpdateDto treatmentUpdateDto, Guid treatmentId, string email);
}