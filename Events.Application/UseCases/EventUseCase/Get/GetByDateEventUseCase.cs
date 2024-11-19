using AutoMapper;
using Events.Application.DTO.Event;
using Events.Application.DTO.Page;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Event;
using Events.Application.Validation.Page;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.EventUseCase.Get
{
    public class GetByDateEventUseCase : IGetByDateEventUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetByDateEventUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EventDTO>> ExecuteAsync(int pageNumber, int pageSize, DateTime dateTime, CancellationToken cancellationToken)
        {
            var pageDTO = new PageDTO() { PageNumber = pageNumber, PageSize = pageSize };

            var validator = new PageValidator();

            var validationResult = validator.Validate(pageDTO);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString());
            }

            var collection = await unitOfWork.EventRepository.GetByDateAsync(pageDTO.PageNumber, pageDTO.PageSize, dateTime, cancellationToken);

            return mapper.Map<IEnumerable<EventDTO>>(collection);
        }
    }
}
