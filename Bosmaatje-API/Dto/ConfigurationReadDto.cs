namespace Bosmaatje_API.Dto
{
    public class ConfigurationReadDto
    {
        public required Guid configurationId { get; set; }
        public required string childName { get; set; }
        public required DateTime ChildBirthDate { get; set; }
        public required string PrimaryDoctorName { get; set; }
        public required string CharacterId { get; set; }
        public required string TreatmentPlanName { get; set; }
    }
}


