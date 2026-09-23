using Microsoft.EntityFrameworkCore;
using sat_ground_station.Controllers;
using sat_ground_station.Repository;
using sat_ground_station.Service;
using System.Text.Json.Serialization;

DotNetEnv.Env.Load();
DotNetEnv.Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter()
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TelemetryService>();
builder.Services.AddScoped<CheckSeverity>();
builder.Services.AddHostedService<TcpRecieverService>();

var connectionString = (string) Environment.GetEnvironmentVariable("SGS_POSTGRES_CONNECTION_STRING");

if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("SGS_POSTGRES_CONNECTION_STRING is not set or empty. Skipping DbContext registration.");
}
builder.Services.AddDbContext<AppDbContext>(options =>
{
     options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    //app.UseHttpsRedirection();
}

using (var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider;
    var db = provider.GetService<sat_ground_station.Repository.AppDbContext>();

    if (db != null)
    {
        Console.WriteLine(db.Database.CanConnect());
    }
    else
    {
        Console.WriteLine("AppDbContext not registered because no connection string was provided.");
    }
}

app.UseAuthorization();

app.MapControllers();

app.Run();
