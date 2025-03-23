namespace Bosmaatje_API.Dto
{
    public class TreatmentUpdateDto
    {
        public required DateTime? date { get; set; }
        public required string? doctorName { get; set; }
        public required string? stickerId { get; set; }
    }
}

