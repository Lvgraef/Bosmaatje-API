using Bosmaatje_API.Dto;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Repository;

public class DiaryRepository(string sqlConnectionString) : IDiaryRepository
{
    public async Task Create(DiaryCreateDto diaryCreateDto, string email)
    {
        await using var sqlConnection = new SqlConnection(sqlConnectionString);
        await sqlConnection.ExecuteAsync($"INSERT INTO [Diary] (Date, Content, Email) VALUES (@date, @content, @email)", 
            new
            {
                email, diaryCreateDto.date, diaryCreateDto.content
            });
    }

    public async Task<DiaryReadDto?> Read(string email)
    {
        await using var sqlConnection = new SqlConnection(sqlConnectionString);
        var result = await sqlConnection.QuerySingleOrDefaultAsync("SELECT * FROM [Diary] WHERE Email = @email",
            new 
            {
                email
            });
        return result;
    }

    public async Task Update(DiaryUpdateDto diaryUpdateDto, string email)
    {
        await using var sqlConnection = new SqlConnection(sqlConnectionString);
        await sqlConnection.ExecuteAsync("UPDATE [Diary] SET Content = @content WHERE Email = @email",
            new
            {
                email, diaryUpdateDto.content
            });
    }

    public async Task Delete(string email)
    {
        await using var sqlConnection = new SqlConnection(sqlConnectionString);
        await sqlConnection.ExecuteAsync("DELETE FROM [Diary] WHERE Email = @email", 
            new
            {
              email
            });
    }
}