using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sat_ground_station.Models;
using sat_ground_station.Repository;

namespace sat_ground_station.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertController : ControllerBase
{

    private readonly AppDbContext _dbContext;

    public AlertController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Alert>>> GetMostRecentAlerts()
    {

        int limit = 100;

        List<Alert> alert = await _dbContext.Alerts.OrderByDescending( a => a.Id).Take(limit).ToListAsync();

        if(alert == null || alert.Count == 0)
        {
            return NotFound();
        }

        return Ok(alert);
    }
}
