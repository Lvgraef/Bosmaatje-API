using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(connectionString);

// Sample
// builder.Services.AddSingleton<I*item*Repository<Guid, *other things you may need*>, *item*Repository>(_ => new *item*Repository(connectionString ?? throw new ArgumentException("No connection string found in secrets.json")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();

var requireUserPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddAuthorizationBuilder()
    .SetDefaultPolicy(requireUserPolicy)
    .SetFallbackPolicy(requireUserPolicy);

builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 10;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
    })
    .AddRoles<IdentityRole>()
    .AddDapperStores(options =>
    {
        options.ConnectionString = builder.Configuration.GetConnectionString("DapperIdentity");
    });

builder.Services.AddMvc().AddJsonOptions(options => { options.JsonSerializerOptions.MaxDepth = 64; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGroup("/account").MapIdentityApi<IdentityUser>().AllowAnonymous();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); //.RequireAuthorization();

app.MapGet("/",
    () => $"The API is up and running. Connection string found: {(sqlConnectionStringFound ? "Yes" : "No")}");
app.MapPost("/account/logout",
    async (SignInManager<IdentityUser> signInManager, [FromBody] object? empty) =>
    {
        if (empty == null) return Results.Unauthorized();
        await signInManager.SignOutAsync();
        return Results.Ok();
    });

app.Run();