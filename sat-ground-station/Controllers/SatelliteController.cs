using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sat_ground_station.Models;
using sat_ground_station.Models.Dto;
using sat_ground_station.Repository;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 

namespace sat_ground_station.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SatelliteController : ControllerBase
{

    private readonly AppDbContext _dbContext;

    public SatelliteController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Satellite>>> GetMostRecentSatellites()
    {
        int limit = 100;

        List<Satellite> satellites = await _dbContext.Satellites.Include(s => s.Mission).OrderByDescending(s => s.Id).Take(limit).ToListAsync();

        if(satellites == null || satellites.Count == 0)
        {
            return NotFound();
        }
        
        return Ok(satellites);
    }

    [HttpGet("{satelliteId}")]
    public async Task<ActionResult<List<Satellite>>> GetSatelliteById(int satelliteId)
    {
        Satellite? satellite = await _dbContext.Satellites.Include(s => s.Mission).FirstOrDefaultAsync(s => s.Id == satelliteId);

        if (satellite == null)
        {
            return NotFound();
        }

        return Ok(satellite);
    }

    [HttpPost]
    public async Task<ActionResult<Satellite>> CreateSatellite([FromBody] Satellite sat)
    {

        if(sat == null)
        {
            return BadRequest("Invalid Request");
        }

        await _dbContext.Satellites.AddAsync(sat);
        await _dbContext.SaveChangesAsync();

        return Ok(sat);
    }

    [HttpPut("{satelliteId}")]
    public async Task<ActionResult<Satellite>> UpdateSatellite(int satelliteId, [FromBody] UpdateSatelliteObject updatedSatellite)
    {
        Satellite? satellite = await _dbContext.Satellites.FirstOrDefaultAsync(s => s.Id == satelliteId);

        if (satellite == null)
        {
            return NotFound();
        }

        if (updatedSatellite.Status != null){
            satellite.Status = updatedSatellite.Status.Value;
        }

        if (updatedSatellite.MissionId != null)
        {
            satellite.MissionId = updatedSatellite.MissionId.Value;
        }

        await _dbContext.SaveChangesAsync();

        return Ok(satellite);
    }

}
