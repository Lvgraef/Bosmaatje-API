namespace Bosmaatje_API.Dto
{
    public class DiaryCreateDto
    {
        public required DateTime date { get; set; }
        public required string content { get; set; }
        public required string email { get; set; }
    }
}

