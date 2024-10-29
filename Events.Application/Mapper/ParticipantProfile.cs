using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Domain.Models;

namespace Events.Application.Mapper
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            CreateMap<Participant, ParticipantDTO>()
                .ReverseMap();
            CreateMap<Participant, ParticipantAuthDTO>()
                .ReverseMap();
        }
    }
}
