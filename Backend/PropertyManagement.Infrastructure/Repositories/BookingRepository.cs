using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PropertyManagement.Infrastructure.Repositories;

public class BookingRepository : Repository<Booking>, IBookingRepository
{
    public BookingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Booking>> GetByPropertyIdAsync(int propertyId)
    {
        return await _context.Bookings.Where(b => b.PropertyId == propertyId).ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetByUserIdAsync(int userId)
    {
        return await _context.Bookings.Where(b => b.UserId == userId).ToListAsync();
    }
}
