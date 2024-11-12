using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Exceptions;
using Events.Application.UseCases.ParticipantUseCase.Get.Interfaces;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.ParticipantUseCase.Get
{
    public class GetByRefreshTokenParticipantUseCase : IGetByRefreshTokenParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetByRefreshTokenParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ParticipantDTO> ExecuteAsync(string refreshToken, CancellationToken cancellationToken)
        {
            if (refreshToken is null)
            {
                throw new BadRequestException("Invalid Token");

            }
            var participant = await unitOfWork.ParticipantRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken);

            if (participant is null)
            {
                throw new NotFoundException("Invalid get refresh token operation! Participant not found");
            }

            return mapper.Map<ParticipantDTO>(participant);
        }
    }
}
