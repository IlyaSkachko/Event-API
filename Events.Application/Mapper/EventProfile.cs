using AutoMapper;
using Events.Application.DTO.Event;
using Events.Domain.Models;

namespace Events.Application.Mapper
{
    public class EventProfile : Profile
    {
        public EventProfile() 
        {
            CreateMap<Event, EventDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
            CreateMap<Event, EventImageDTO>().ReverseMap();
        }
    }
}
