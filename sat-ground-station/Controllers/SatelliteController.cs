using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using sat_ground_station.Models;
using sat_ground_station.Models.Dto;
using sat_ground_station.Models.Enums;
using sat_ground_station.Network;
using sat_ground_station.Repository;
using sat_sim.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    private readonly IHubContext<SignalRServerHub> _hubContext;

    public SatelliteController(AppDbContext dbContext, IHubContext<SignalRServerHub> hubContext)
    {
        _dbContext = dbContext;
        _hubContext = hubContext;
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

        SatelliteCommand? command = new SatelliteCommand(sat, Commands.Add);
        await _hubContext.Clients.All.SendAsync("ModifySatellite", command);

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

        if (updatedSatellite == null)
        {
            return BadRequest("Update data is required");
        }

        if (satellite.Status == Status.Decommissioned)
        {
            return BadRequest("Satellite is Decommissioned");
        }

        Status status = Status.Active;

        if (updatedSatellite.Command != null){

            if(updatedSatellite.Command == Commands.Activate || updatedSatellite.Command == Commands.Add)
            {
                status = Status.Active;
            }
            else if(updatedSatellite.Command == Commands.Deactivate)
            {
                status = Status.Inactive;
            }
            else if(updatedSatellite.Command == Commands.Delete)
            {
                status = Status.Decommissioned;
            }
        }

        if (updatedSatellite.MissionId != null)
        {
            satellite.MissionId = updatedSatellite.MissionId.Value;
        }

        if (satellite.Status == status)
        {
            return Ok(satellite);
        }

        satellite.Status = status;

        if (updatedSatellite.Command != null)
        {
            SatelliteCommand? command = new SatelliteCommand(satellite, updatedSatellite.Command.Value);
            await _hubContext.Clients.All.SendAsync("ModifySatellite", command);
        }

        await _dbContext.SaveChangesAsync();

        return Ok(satellite);
    }

}
