using Events.Application.DTO.Participant;
using Events.Application.Exceptions;
using Events.Application.UseCases.ParticipantUseCase.Delete.Interfaces;
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
            if (dto is null)
            {
                throw new BadRequestException("Participant data is missing");
            }

            var participant = await unitOfWork.ParticipantRepository.GetByIdAsync(dto.Id, cancellationToken);

            if (participant is null)
            {
                throw new NotFoundException("Invalid delete refresh token operation! Participant is not found!");
            }

            participant.RefreshToken = "";

            try
            {
                await unitOfWork.ParticipantRepository.UpdateAsync(participant, cancellationToken);
            }

            catch(InvalidOperationException)
            {
                throw new NotFoundException("Invalid update operation! This event participant does not exist");
            }
        }
    }
}
