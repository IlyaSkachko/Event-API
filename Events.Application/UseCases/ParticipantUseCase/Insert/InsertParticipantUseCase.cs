using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Hash;
using Events.Application.Interfaces.UseCase.Participant;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.ParticipantUseCase.Insert
{
    public class InsertParticipantUseCase : IInsertParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public InsertParticipantUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CreateParticipantDTO dto, byte[] salt, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Participant>(dto);

            entity.PasswordSalt = salt;

            await unitOfWork.ParticipantRepository.InsertAsync(entity, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
