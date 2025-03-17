using System;

namespace Dto
{
    public class ConfigurationReadDto(Guid configurationId, string childName, DateTime childBirthDate, string primaryDoctorName, string characterId, string treatmentPlanName)
    {
        public required Guid ConfigurationId { get; set; } = configurationId;
        public required string ChildName { get; set; } = childName;
        public required DateTime ChildBirthDate { get; set; } = childBirthDate;
        public required string PrimaryDoctorName { get; set; } = primaryDoctorName;
        public required string CharacterId { get; set; } = characterId;
        public required string TreatmentPlanName { get; set; } = treatmentPlanName;
    }
}


