using AutoMapper;
using Events.Application.DTO.Category;
using Events.Application.Exceptions;
using Events.Application.UseCases.CategoryUseCase.Update.Interfaces;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.CategoryUseCase.Update
{
    public class UpdateCategoryUseCase : IUpdateCategoryUseCase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateCategoryUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CategoryDTO dto, CancellationToken cancellationToken)
        {
            if (dto is null)
            {
                throw new BadRequestException("Category data is missing");
            }

            try
            {
                var category = mapper.Map<Category>(dto);

                await unitOfWork.CategoryRepository.UpdateAsync(category, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Invalid update operation! This category does not exist");
            }
        }
    }
}
