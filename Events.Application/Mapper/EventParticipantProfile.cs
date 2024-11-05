using AutoMapper;
using Events.Application.DTO.EventParticipant;
using Events.Domain.Models;

namespace Events.Application.Mapper
{
    public class EventParticipantProfile : Profile
    {
        public EventParticipantProfile() 
        {
            CreateMap<EventParticipantDTO, EventParticipant>()
                .ReverseMap();
        }
    }
}
