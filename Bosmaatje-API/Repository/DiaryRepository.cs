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
                email, 
                diaryCreateDto.date, 
                diaryCreateDto.content
            });
    }

    public async Task<List<DiaryReadDto>> Read(string email, DateTime? date)
    {
        await using var sqlConnection = new SqlConnection(sqlConnectionString);
        if (date == null)
        {
            var resultList = await sqlConnection.QueryAsync<DiaryReadDto>("SELECT * FROM [Diary] WHERE Email = @email",
                new 
                {
                    email
                });
            return resultList.ToList();
        }
        var result = await sqlConnection.QueryAsync<DiaryReadDto>("SELECT * FROM [Diary] WHERE Email = @email AND Date = @date",
            new 
            {
                email,
                date
            });
        return result.ToList();
    }

    public async Task Update(DiaryUpdateDto diaryUpdateDto, string email, DateTime date)
    {
        await using var sqlConnection = new SqlConnection(sqlConnectionString);

        await sqlConnection.ExecuteAsync(
            "UPDATE [Diary] SET Content = @content WHERE Email = @email AND Date = @date",
            new
            {
                email,
                content = diaryUpdateDto.content,
                date = date

            });

    }

    public async Task Delete(string email, DateTime date)
    {
        await using var sqlConnection = new SqlConnection(sqlConnectionString);
        await sqlConnection.ExecuteAsync("DELETE FROM [Diary] WHERE Email = @email AND [Date] = @date", 
            new
            {
              email, date
            });
    }
}