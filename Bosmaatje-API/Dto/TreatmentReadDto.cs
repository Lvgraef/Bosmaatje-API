namespace Bosmaatje_API.Dto
{
    public class TreatmentReadDto
    {
        public Guid treatmentId { get; set; }
        public required string treatmentName { get; set; }
        public required string imagePath { get; set; }
        public required string? videoPath { get; set; }
        public required DateTime? date { get; set; }
        public required int order { get; set; }
        public required string doctorName { get; set; }
        public required List<string> description { get; set; }
        public required bool isCompleted { get; set; }
        public required string? stickerId { get; set; }
    }
}

