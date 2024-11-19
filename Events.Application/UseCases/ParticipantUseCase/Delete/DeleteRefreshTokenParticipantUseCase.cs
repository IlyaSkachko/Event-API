using Events.Application.DTO.Participant;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Participant;
using Events.Domain.Interfaces.UOW;
using System.Diagnostics;

namespace Events.Application.UseCases.ParticipantUseCase.Delete
{
    public class DeleteRefreshTokenParticipantUseCase : IDeleteRefreshTokenParticipantUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteRefreshTokenParticipantUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(ParticipantDTO dto, CancellationToken cancellationToken)
        {
            var participant = await unitOfWork.ParticipantRepository.GetByIdAsync(dto.Id, cancellationToken);

            participant.RefreshToken = "";

            await unitOfWork.ParticipantRepository.UpdateAsync(participant, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
