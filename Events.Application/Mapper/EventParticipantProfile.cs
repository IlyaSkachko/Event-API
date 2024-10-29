using AutoMapper;
using Events.Application.DTO.EventParticipant;
using Events.Domain.Models;

namespace Events.Application.Mapper
{
    public class EventParticipantProfile : Profile
    {
        public EventParticipantProfile() 
        {
            CreateMap<EventParticipant, EventParticipantDTO>()
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.Name))
                .ForMember(dest => dest.ParticipantName, opt => opt.MapFrom(src => src.Participant.Name + " " + src.Participant.Surname))
                .ReverseMap();
        }
    }
}
