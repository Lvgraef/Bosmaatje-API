using System;

namespace Models
{
    public class Configuration(string childName, DateTime childBirthDate, string headDoctorName, string characterId)
    { 
        public string ChildName { get; set; } = childName; 
        public DateTime ChildBirthDate { get; set; } = childBirthDate;
        public string HeadDoctorName { get; set; } = headDoctorName; 
        public string CharacterId { get; set; } = characterId; 
    }
}