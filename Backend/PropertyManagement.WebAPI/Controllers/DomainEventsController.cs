using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Services.DomainEvents;

namespace PropertyManagement.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DomainEventsController : ControllerBase
{
    private readonly IDomainEventService _domainEventService;

    public DomainEventsController(IDomainEventService domainEventService)
    {
        _domainEventService = domainEventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDomainEvents([FromQuery] int? propertyId, [FromQuery] string? eventType, [FromQuery] DateTime? occurredAt, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var domainEvents = await _domainEventService.GetDomainEventsAsync(propertyId, eventType, occurredAt, pageNumber, pageSize);
        return Ok(domainEvents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDomainEvent(int id)
    {
        var domainEvent = await _domainEventService.GetDomainEventByIdAsync(id);
        return Ok(domainEvent);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDomainEvent([FromBody] DomainEventDto domainEventDto)
    {
        var domainEvent = await _domainEventService.CreateDomainEventAsync(domainEventDto);
        return CreatedAtAction(nameof(GetDomainEvent), new { id = domainEvent.Id }, domainEvent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDomainEvent(int id)
    {
        await _domainEventService.DeleteDomainEventAsync(id);
        return NoContent();
    }
}
