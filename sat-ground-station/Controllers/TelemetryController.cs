using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sat_ground_station.Models;
using sat_ground_station.Repository;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 

namespace sat_ground_station.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelemetryController : ControllerBase
{

    private readonly AppDbContext _dbContext;

    public TelemetryController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Telemetry>>> GetMostRecentTelemetries()
    {
        int limit = 100;

        List<Telemetry> telemetries =  await _dbContext.Telemetries.OrderByDescending(t => t.Timestamp).Take(limit).ToListAsync();

        if (telemetries == null || telemetries.Count == 0)
        {
            return NotFound();
        }

        return Ok(telemetries);
    }

    [HttpGet("{satelliteId}")]
    public async Task<ActionResult<List<Telemetry>>> GetMostRecentTelemetriesBySatelliteId(int satelliteId)
    {
        int limit = 100;

        if (satelliteId == null) return NotFound();

        List<Telemetry> telemetries =  await _dbContext.Telemetries.Where(t => t.SatelliteId == satelliteId).OrderByDescending(t => t.SatelliteId == satelliteId).Take(limit).ToListAsync();

        if(telemetries == null || telemetries.Count == 0)
        {
            return NotFound();
        }

        return Ok(telemetries);
    }

    [HttpPost]
    public async Task<ActionResult> AddTelemetry([FromBody] Telemetry tel)
    {

        if (tel == null) return BadRequest("Invalid Request");

        await _dbContext.Telemetries.AddAsync(tel);
        await _dbContext.SaveChangesAsync();

        return Ok(tel);
    }

}
