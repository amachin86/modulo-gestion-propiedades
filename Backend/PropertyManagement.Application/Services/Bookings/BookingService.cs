using AutoMapper;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Services.Bookings;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookingDto>> GetBookingsAsync(int? propertyId = null, DateTime? checkIn = null, DateTime? checkOut = null, int pageNumber = 1, int pageSize = 10)
    {
        var bookingRepository = _unitOfWork.Repository<Booking>();
        var bookings = await bookingRepository.GetAllAsync();

        // Apply filters
        if (propertyId.HasValue)
        {
            bookings = bookings.Where(b => b.PropertyId == propertyId.Value);
        }

        if (checkIn.HasValue)
        {
            bookings = bookings.Where(b => b.CheckIn >= checkIn.Value);
        }

        if (checkOut.HasValue)
        {
            bookings = bookings.Where(b => b.CheckOut <= checkOut.Value);
        }

        // Apply pagination
        bookings = bookings.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return _mapper.Map<IEnumerable<BookingDto>>(bookings);
    }

    public async Task<BookingDto> GetBookingByIdAsync(int id)
    {
        var bookingRepository = _unitOfWork.Repository<Booking>();
        var booking = await bookingRepository.GetByIdAsync(id);

        if (booking == null)
        {
            throw new KeyNotFoundException($"Booking with ID {id} not found.");
        }

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<BookingDto> CreateBookingAsync(CreateBookingDto createBookingDto)
    {
        var booking = _mapper.Map<Booking>(createBookingDto);

        var bookingRepository = _unitOfWork.Repository<Booking>();
        await bookingRepository.AddAsync(booking);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<BookingDto> UpdateBookingAsync(int id, UpdateBookingDto updateBookingDto)
    {
        var bookingRepository = _unitOfWork.Repository<Booking>();
        var booking = await bookingRepository.GetByIdAsync(id);

        if (booking == null)
        {
            throw new KeyNotFoundException($"Booking with ID {id} not found.");
        }

        _mapper.Map(updateBookingDto, booking);
        bookingRepository.Update(booking);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task DeleteBookingAsync(int id)
    {
        var bookingRepository = _unitOfWork.Repository<Booking>();
        var booking = await bookingRepository.GetByIdAsync(id);

        if (booking == null)
        {
            throw new KeyNotFoundException($"Booking with ID {id} not found.");
        }

        bookingRepository.Remove(booking);
        await _unitOfWork.SaveChangesAsync();
    }
}
