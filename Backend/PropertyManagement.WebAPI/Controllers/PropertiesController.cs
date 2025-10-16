using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Services.Properties;

namespace PropertyManagement.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProperties()
    {
        var result = await _propertyService.GetPropertiesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProperty(int id)
    {
        var result = await _propertyService.GetPropertyByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyDto dto)
    {
        var result = await _propertyService.CreatePropertyAsync(dto);
        return CreatedAtAction(nameof(GetProperty), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProperty(int id, [FromBody] UpdatePropertyDto dto)
    {
        var result = await _propertyService.UpdatePropertyAsync(id, dto);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(int id)
    {
        await _propertyService.DeletePropertyAsync(id);
        return NoContent();
    }
}
