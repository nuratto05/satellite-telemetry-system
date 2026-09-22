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
public class MissionController : ControllerBase
{

    private readonly AppDbContext _dbContext;

    public MissionController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Mission>>> GetMostRecentMissions()
    {
        int limit = 100;

        List<Mission> missions = await _dbContext.Missions.Include(m => m.Satellites).OrderBy(m => m.Id).Take(limit).ToListAsync();

        if (missions == null || missions.Count == 0)
        {
            return NotFound();
        }

        return Ok(missions);
    }

    [HttpGet("{missionId}")]
    public async Task<ActionResult<List<Mission>>> GetMissionsBySateliteId(int missionId)
    {
        Mission? mission = await _dbContext.Missions.Include(m => m.Satellites).FirstOrDefaultAsync(m => m.Id == missionId);

        if (mission == null)
        {
            return NotFound();
        }

        return Ok(mission);
    }

    [HttpPost]
    public async Task<ActionResult<Mission>> CreateMission([FromBody] Mission mission)
    {

        if (mission == null)
        {
            return BadRequest("Invalid Request");
        }

        await _dbContext.Missions.AddAsync(mission);
        await _dbContext.SaveChangesAsync();

        return Ok(mission);
    }

    [HttpPut("{missionId}")]
    public async Task<ActionResult<Mission>> UpdateMission(int missionId, [FromBody] UpdateMissionObject updatedMission)
    {
        Mission? mission = await _dbContext.Missions.FirstOrDefaultAsync(m => m.Id == missionId);

        if (mission == null)
        {
            return NotFound();
        }

        if (updatedMission.Name != null)
        {
            mission.Name = updatedMission.Name;
        }

        if (updatedMission.Description != null){
            mission.Description = updatedMission.Description;
        }

        if (updatedMission.Status != null){
            mission.Status = updatedMission.Status.Value;
        }

        await _dbContext.SaveChangesAsync();

        return Ok(mission);
    }

}
