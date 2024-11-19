using AutoMapper;
using Events.Application.DTO.Category;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Category;
using Events.Application.Validation.Category;
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

        public async Task ExecuteAsync(CategoryDTO categoryDTO, CancellationToken cancellationToken)
        {
            var validator = new CategoryValidator();

            var validationResult = validator.Validate(categoryDTO);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString());
            }

            var category = mapper.Map<Category>(categoryDTO);

            await unitOfWork.CategoryRepository.UpdateAsync(category, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
