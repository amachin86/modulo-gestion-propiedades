using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Application.Services.Sync;

namespace PropertyManagement.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SyncController : ControllerBase
{
    private readonly ISyncService _syncService;

    public SyncController(ISyncService syncService)
    {
        _syncService = syncService;
    }

    [HttpPost("ota")]
    public async Task<IActionResult> SyncOTA([FromQuery] int propertyId, [FromQuery] string action)
    {
        await _syncService.SyncPropertyAsync(propertyId, action);
        return Ok(new { message = "Sync event registered successfully" });
    }
}
