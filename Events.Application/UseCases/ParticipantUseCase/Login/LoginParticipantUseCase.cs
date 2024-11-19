using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Interfaces.UseCase.Participant;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.ParticipantUseCase.Login
{
    public class LoginParticipantUseCase : ILoginParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public LoginParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(ParticipantAuthDTO dto, string refreshToken, CancellationToken cancellationToken)
        {
            var participant = await unitOfWork.ParticipantRepository.GetByEmailAsync(dto.Email, cancellationToken);

            participant.RefreshToken = refreshToken;

            await unitOfWork.ParticipantRepository.UpdateAsync(participant, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
