using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Services.Hosts;

namespace PropertyManagement.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class HostsController : ControllerBase
{
    private readonly IHostService _hostService;

    public HostsController(IHostService hostService)
    {
        _hostService = hostService;
    }

    [HttpGet]
    public async Task<IActionResult> GetHosts([FromQuery] string? name, [FromQuery] string? email, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var hosts = await _hostService.GetHostsAsync(name, email, pageNumber, pageSize);
        return Ok(hosts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHost(int id)
    {
        var host = await _hostService.GetHostByIdAsync(id);
        return Ok(host);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHost([FromBody] CreateHostDto createHostDto)
    {
        var host = await _hostService.CreateHostAsync(createHostDto);
        return CreatedAtAction(nameof(GetHost), new { id = host.Id }, host);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHost(int id, [FromBody] UpdateHostDto updateHostDto)
    {
        var host = await _hostService.UpdateHostAsync(id, updateHostDto);
        return Ok(host);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHost(int id)
    {
        await _hostService.DeleteHostAsync(id);
        return NoContent();
    }
}
