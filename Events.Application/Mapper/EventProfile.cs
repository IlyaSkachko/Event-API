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
                .ReverseMap();
            CreateMap<EventDTO, UpdateEventDTO>()
                .ReverseMap();
        }
    }
}
