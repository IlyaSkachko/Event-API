using AutoMapper;
using Events.Application.DTO.EventParticipant;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.EventParticipant;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.EventParticipantUseCase.Update
{
    public class UpdateEventParticipantUseCase : IUpdateEventParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateEventParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(EventParticipantDTO dto, CancellationToken cancellationToken)
        {
            var obj = mapper.Map<EventParticipant>(dto);

            await unitOfWork.EventParticipantRepository.UpdateAsync(obj, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
