using Microsoft.EntityFrameworkCore;
using sat_ground_station.Controllers;
using sat_ground_station.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContext>(options =>
{
     options.UseNpgsql("Host=localhost;Port=5432;Database=telSystemDb;Username=postgres;Password=shiba");
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
        Console.WriteLine("TelemetryRepository not registered because no connection string was provided.");
    }
}

app.UseAuthorization();

app.MapControllers();

app.Run();
