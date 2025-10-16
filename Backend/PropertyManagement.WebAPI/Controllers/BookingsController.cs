using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Services.Bookings;

namespace PropertyManagement.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings([FromQuery] int? propertyId, [FromQuery] DateTime? checkIn, [FromQuery] DateTime? checkOut, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var bookings = await _bookingService.GetBookingsAsync(propertyId, checkIn, checkOut, pageNumber, pageSize);
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBooking(int id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);
        return Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto createBookingDto)
    {
        var booking = await _bookingService.CreateBookingAsync(createBookingDto);
        return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBooking(int id, [FromBody] UpdateBookingDto updateBookingDto)
    {
        var booking = await _bookingService.UpdateBookingAsync(id, updateBookingDto);
        return Ok(booking);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        await _bookingService.DeleteBookingAsync(id);
        return NoContent();
    }
}
