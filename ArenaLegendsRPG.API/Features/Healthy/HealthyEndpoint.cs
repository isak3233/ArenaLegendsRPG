using Microsoft.AspNetCore.Mvc;

namespace ArenaLegendsRPG.API.Features.Healthy;

[ApiController]
[Route("healthy")]
public class HealthyController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}