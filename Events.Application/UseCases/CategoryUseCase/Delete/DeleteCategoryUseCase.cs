using AutoMapper;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Category;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.UseCases.CategoryUseCase.Delete
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id, cancellationToken);

            await unitOfWork.CategoryRepository.DeleteAsync(category, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
