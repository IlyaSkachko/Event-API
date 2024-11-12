using AutoMapper;
using Events.Application.DTO.Participant;
using Events.Application.Exceptions;
using Events.Application.UseCases.HashUseCase.Hash.Interfaces;
using Events.Application.UseCases.ParticipantUseCase.Insert.Interfaces;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.ParticipantUseCase.Insert
{
    public class InsertParticipantUseCase : IInsertParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IHashPasswordUseCase hashPasswordUseCase;
        private readonly IUnitOfWork unitOfWork;

        public InsertParticipantUseCase(IMapper mapper, IHashPasswordUseCase hashPasswordUseCase, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.hashPasswordUseCase = hashPasswordUseCase;
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CreateParticipantDTO dto, CancellationToken cancellationToken)
        {
            if (dto is null)
            {
                throw new BadRequestException("Participant data is missing");
            }

            var participant = await unitOfWork.ParticipantRepository.GetByEmailAsync(dto.Email, cancellationToken);

            if (participant is not null)
            {
                throw new AlreadyExistException("Invalid insert operation! This participant already exist");
            }

            var entity = mapper.Map<Participant>(dto);

            entity.Password = hashPasswordUseCase.Execute(entity.Password, out byte[] salt);

            entity.PasswordSalt = salt;

            try
            {
                await unitOfWork.ParticipantRepository.InsertAsync(entity, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new AlreadyExistException("Invalid insert operation! This participant already exist");
            }
        }
    }
}
