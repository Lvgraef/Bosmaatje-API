namespace Bosmaatje_API.Dto
{
    public class ConfigurationReadDto
    {
        public required Guid configurationId { get; set; }
        public required string childName { get; set; }
        public required DateTime childBirthDate { get; set; }
        public required string primaryDoctorName { get; set; }
        public required string characterId { get; set; }
        public required string treatmentPlanName { get; set; }
    }
}


