using Bosmaatje_API.Dto;

namespace Bosmaatje_API.IRepository;

public interface ITreatmentRepository
{
    Task<List<TreatmentReadDto?>> Read(string email, string treatmentPlanName);
    Task Update(TreatmentUpdateDto treatmentUpdateDto, string treatmentPlanName);
}