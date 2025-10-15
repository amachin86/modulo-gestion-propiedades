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
    }
}
