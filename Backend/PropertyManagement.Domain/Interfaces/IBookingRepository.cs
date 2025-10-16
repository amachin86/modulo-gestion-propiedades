using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Interfaces;

public interface IBookingRepository : IRepository<Booking>
{
    Task<IEnumerable<Booking>> GetByPropertyIdAsync(int propertyId);
    Task<IEnumerable<Booking>> GetByUserIdAsync(int userId);
}
