namespace Bosmaatje_API.Dto
{
    public class ConfigurationCreateDto
    { 
        public required string ChildName { get; set; }
        public required DateTime ChildBirthDate { get; set; }
        public required string PrimaryDoctorName { get; set; }  
        public required string CharacterId { get; set; } 
        public required string TreatmentPlanName { get; set; } 
    }
}