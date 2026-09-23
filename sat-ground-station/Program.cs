using Microsoft.EntityFrameworkCore;
using sat_ground_station.Controllers;
using sat_ground_station.Network;
using sat_ground_station.Repository;
using sat_ground_station.Service;
using System.Text.Json.Serialization;

DotNetEnv.Env.Load();
DotNetEnv.Env.TraversePath().Load();


var connectionString = (string)Environment.GetEnvironmentVariable("SGS_POSTGRES_CONNECTION_STRING");
var signaleRendpointKey = (string)Environment.GetEnvironmentVariable("SIGNALR_ENDPOINT_KEY");


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
builder.Services.AddSignalR();

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

app.MapHub<SignalRServerHub>($"/simulationHub/{signaleRendpointKey}");

app.UseAuthorization();

app.MapControllers();

app.Run();
