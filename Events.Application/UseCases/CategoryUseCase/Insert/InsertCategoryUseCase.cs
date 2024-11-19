using AutoMapper;
using Events.Application.DTO.Category;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Category;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.CategoryUseCase.Insert
{
    public class InsertCategoryUseCase : IInsertCategoryUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public InsertCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task ExecuteAsync(CreateCategoryDTO dto, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Category>(dto);

            await unitOfWork.CategoryRepository.InsertAsync(category, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
