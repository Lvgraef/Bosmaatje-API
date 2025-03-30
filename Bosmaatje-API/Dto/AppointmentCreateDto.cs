namespace Bosmaatje_API.Dto
{
    public class AppointmentCreateDto
    {
        public required string name { get; set; }
        public required string email { get; set; }
        public required DateTime date { get; set; }
    }
}

