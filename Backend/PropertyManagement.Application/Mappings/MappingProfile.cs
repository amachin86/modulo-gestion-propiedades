using AutoMapper;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Property, PropertyDto>();
        CreateMap<CreatePropertyDto, Property>();
        CreateMap<UpdatePropertyDto, Property>();

        CreateMap<Host, HostDto>();
        CreateMap<CreateHostDto, Host>();
        CreateMap<UpdateHostDto, Host>();

        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();

        CreateMap<Booking, BookingDto>();
        CreateMap<CreateBookingDto, Booking>();
        CreateMap<UpdateBookingDto, Booking>();

        CreateMap<DomainEvent, DomainEventDto>();
    }
}
