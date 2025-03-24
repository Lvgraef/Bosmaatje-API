using Bosmaatje_API.Dto;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Repository;

public class PhotoRepository(string sqlConnectionString) : IPhotoRepository
{
    public async Task Create(PhotoCreateDto photoCreateDto)
    {
        await using var sqlConnection = new SqlConnection(sqlConnectionString);
        await sqlConnection.ExecuteAsync("INSERT INTO [Photo] (PhotoId, PhotoPath, Email) VALUES(@photoId, @photoPath, @email)",
            new
            {
                photoCreateDto.photoId, 
                photoCreateDto.photoPath, 
                photoCreateDto.email
            });
    }

    public async Task<PhotoReadDto> Read(string email)
    {
        await using var sqlConnection = new SqlConnection(sqlConnectionString);
        var result = await sqlConnection.QuerySingleOrDefaultAsync("SElECT * FROM [Photo] WHERE Email = @email",
            new
            {
                email
            });
        return result;
    }

    public async Task Delete(string email)
    {
        await using var sqlConnection = new SqlConnection(sqlConnectionString);
        var result = await sqlConnection.ExecuteAsync("DELETE FROM [Photo] WHERE Email = @email",
            new
            {
                email
            });
    }
}