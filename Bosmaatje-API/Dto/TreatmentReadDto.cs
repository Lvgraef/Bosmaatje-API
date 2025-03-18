namespace Bosmaatje_API.Dto
{
    public class TreatmentReadDto
    {
        public Guid treatmentId { get; set; }
        public required string name { get; set; }
        public required string imageUrl { get; set; }
        public required DateTime date { get; set; }
        public required int order { get; set; }
        public required string doctorName { get; set; }
        public required string treatmentPlanName { get; set; }
    }
}

