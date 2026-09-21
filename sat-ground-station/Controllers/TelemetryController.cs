using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public IActionResult AddTelemetry()
    {

        

        return Ok("Hello from satellites");
    }

    [HttpPost]
    public IActionResult AddTelemetry()
    {

        

        return Ok("Hello from satellites");
    }

}
