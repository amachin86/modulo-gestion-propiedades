using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Services.Bookings;

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetBookingsAsync(int? propertyId = null, DateTime? checkIn = null, DateTime? checkOut = null, int pageNumber = 1, int pageSize = 10);
    Task<BookingDto> GetBookingByIdAsync(int id);
    Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto);
    Task<BookingDto> UpdateBookingAsync(int id, UpdateBookingDto updateBookingDto);
    Task DeleteBookingAsync(int id);
}
