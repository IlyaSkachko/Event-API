using AutoMapper;
using Events.Application.DTO.Category;
using Events.Application.Exceptions;
using Events.Application.UseCases.CategoryUseCase.Get.Interfaces;
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
            var collection = await unitOfWork.CategoryRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);

            if (collection is null)
            {
                throw new NotFoundException("Categories are not found");
            }

            return mapper.Map<IEnumerable<CategoryDTO>>(collection);
        }
    }
}
