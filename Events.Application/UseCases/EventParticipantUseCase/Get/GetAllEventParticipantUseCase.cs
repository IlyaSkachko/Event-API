using AutoMapper;
using Events.Application.DTO.EventParticipant;
using Events.Application.DTO.Page;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.EventParticipant;
using Events.Application.Validation.Page;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.EventParticipantUseCase.Get
{
    public class GetAllEventParticipantUseCase : IGetAllEventParticipantUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetAllEventParticipantUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EventParticipantDTO>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var pageDTO = new PageDTO() { PageNumber = pageNumber, PageSize = pageSize };

            var validator = new PageValidator();

            var validationResult = validator.Validate(pageDTO);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString());
            }

            var collection = await unitOfWork.EventParticipantRepository.GetAllAsync(pageDTO.PageNumber, pageDTO.PageSize, cancellationToken);

            return mapper.Map<IEnumerable<EventParticipantDTO>>(collection);
        }
    }
}
