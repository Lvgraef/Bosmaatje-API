namespace Bosmaatje_API.Dto
{
    public class PhotoCreateDto
    {
        public required Guid photoId {get;set;}
        public required string photoPath {get;set;}
        public required string email {get;set;}
    }
}

