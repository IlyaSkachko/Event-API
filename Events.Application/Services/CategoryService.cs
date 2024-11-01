using AutoMapper;
using Events.Application.DTO.Category;
using Events.Application.Services.Interfaces;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Models;

namespace Events.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(CategoryDTO dto, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Category>(dto);

            await unitOfWork.CategoryRepository.DeleteAsync(category, cancellationToken);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.CategoryRepository.GetAllAsync(cancellationToken);

            return  mapper.Map<IEnumerable<CategoryDTO>>(collection);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var collection = await unitOfWork.CategoryRepository.GetAllAsync(pageNumber, pageSize, cancellationToken);

            return mapper.Map<IEnumerable<CategoryDTO>>(collection);
        }

        public async Task<CategoryDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id, cancellationToken);

            return mapper.Map<CategoryDTO>(category);
        }

        public async Task InsertAsync(CategoryDTO dto, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Category>(dto);
            
            await unitOfWork.CategoryRepository.InsertAsync(category, cancellationToken);
        }

        public async Task UpdateAsync(CategoryDTO dto, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Category>(dto);

            await unitOfWork.CategoryRepository.UpdateAsync(category, cancellationToken);
        }
    }
}
