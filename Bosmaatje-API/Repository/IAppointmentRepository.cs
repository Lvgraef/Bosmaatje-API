using Bosmaatje_API.Dto;

namespace Bosmaatje_API.Repository;

public interface IAppointmentRepository
{
    Task Create(AppointmentCreateDto appointmentCreateDto, string email);
    Task<List<AppointmentReadDto>> Read(string email);
    Task Delete(Guid appointmentId);
}