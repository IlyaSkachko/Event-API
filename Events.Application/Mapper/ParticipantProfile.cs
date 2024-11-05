using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Domain.Models;

namespace Events.Application.Mapper
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            CreateMap<ParticipantDTO, Participant>()
                .ReverseMap();
            CreateMap<Participant, ParticipantAuthDTO>()
                .ReverseMap();
            CreateMap<Participant, CreateParticipantDTO>()
                .ReverseMap();
            CreateMap<Participant, UpdateParticipantDTO>()
                .ReverseMap();
        }
    }
}
