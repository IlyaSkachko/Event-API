using AutoMapper;
using Events.Application.DTO.Category;
using Events.Application.DTO.Page;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Category;
using Events.Application.Validation.Page;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.CategoryUseCase.Get
{
    public class GetAllCategoryUseCase : IGetAllCategoryUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var pageDTO = new PageDTO() { PageNumber = pageNumber, PageSize = pageSize };

            var validator = new PageValidator();

            var validationResult = validator.Validate(pageDTO);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString());
            }

            var collection = await unitOfWork.CategoryRepository.GetAllAsync(pageDTO.PageNumber, pageDTO.PageSize, cancellationToken);

            return mapper.Map<IEnumerable<CategoryDTO>>(collection);
        }
    }
}
