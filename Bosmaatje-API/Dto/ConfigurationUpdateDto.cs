using System;

namespace Dto
{
    public class ConfgurationUpdateDto
    {
        public required string? PrimaryDoctorName { get; set; } = primaryDoctorName;
        public required string? CharacterId { get; set; } = characterId;
    }
}

