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

    private readonly TelemetryRepository _telemetryRepository;

    public TelemetryController(TelemetryRepository telemetryRepository)
    {
        _telemetryRepository = telemetryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Telemetry>>> Get100MostRecentTelemetries()
    {
        List<Telemetry> telemetries =  await _telemetryRepository.Telemetries.OrderByDescending(t => t.Timestamp).Take(100).ToListAsync();

        if (telemetries == null || telemetries.Count == 0)
        {
            return NotFound();
        }

        return telemetries;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Telemetry>>> Get100SpecificTelemetries(int id)
    {
        if (id == null) return NotFound();

        List<Telemetry> telemetries =  await _telemetryRepository.Telemetries.OrderByDescending(t => t.SatelliteId).Take(100).ToListAsync();

        if(telemetries == null || telemetries.Count == 0)
        {
            return NotFound();
        }

        return telemetries;
    }

    [HttpPost]
    public async Task<ActionResult> AddTelemetry([FromBody] Telemetry tel)
    {

        if (tel == null) return BadRequest("Invalid Request");

        await _telemetryRepository.Telemetries.AddAsync(tel);
        await _telemetryRepository.SaveChangesAsync();

        return Ok(tel);
    }

}
