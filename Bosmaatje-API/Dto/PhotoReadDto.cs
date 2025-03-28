namespace Bosmaatje_API.Dto
{
    public class PhotoReadDto
    {
        public required Guid photoId { get; set; }
        public required string photoPath { get; set; }
    }
}

