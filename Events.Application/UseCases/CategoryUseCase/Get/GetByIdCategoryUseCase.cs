using AutoMapper;
using Events.Application.DTO.Category;
using Events.Application.Exceptions;
using Events.Application.UseCases.CategoryUseCase.Get.Interfaces;
using Events.Domain.Interfaces.UOW;

namespace Events.Application.UseCases.CategoryUseCase.Get
{
    public class GetByIdCategoryUseCase : IGetByIdCategoryUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetByIdCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<CategoryDTO> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var category = await unitOfWork.CategoryRepository.GetByIdAsync(id, cancellationToken);

                return mapper.Map<CategoryDTO>(category);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Category is not found");
            }
        }
    }
}
