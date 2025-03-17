using System;

namespace Dto
{
    public class Configuration(string childName, DateTime childBirthDate, string primaryDoctorName, string characterId, string treatmentPlanName)
    { 
        public required string ChildName { get; set; } = childName; 
        public required DateTime ChildBirthDate { get; set; } = childBirthDate;
        public required string PrimaryDoctorName { get; set; } = primaryDoctorName; 
        public required string CharacterId { get; set; } = characterId;
        public required string TreatmentPlanName { get; set; } = treatmentPlanName;
    }
}