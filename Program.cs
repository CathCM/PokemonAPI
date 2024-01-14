using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
static string GetDatabasePath(WebApplicationBuilder builder, string apiFolderName) {
    string commonAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
    string apiFolder = Path.Combine(commonAppDataPath, apiFolderName);
    if (!Directory.Exists(apiFolder))
    { 
        Directory.CreateDirectory(apiFolder);
    }
    return apiFolder;

}
var pokemonApiPath = Path.Combine(GetDatabasePath(builder,"PokemonAPI"), "PokemonDb.db");
var connectionString = builder.Configuration.GetConnectionString(pokemonApiPath) ?? $"Data Source={pokemonApiPath}";
builder.Services.AddSqlite<PokemonDb>(connectionString);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
