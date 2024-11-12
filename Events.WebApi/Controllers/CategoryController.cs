using Events.Application.DTO.Category;
using Events.Application.UseCases.CategoryUseCase.Delete.Interfaces;
using Events.Application.UseCases.CategoryUseCase.Get.Interfaces;
using Events.Application.UseCases.CategoryUseCase.Insert.Interfaces;
using Events.Application.UseCases.CategoryUseCase.Update.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IGetAllCategoryUseCase getAllCategoryUseCase;
        private readonly IGetByIdCategoryUseCase getByIdCategoryUseCase;
        private readonly IInsertCategoryUseCase insertCategoryUseCase;
        private readonly IUpdateCategoryUseCase updateCategoryUseCase;
        private readonly IDeleteCategoryUseCase deleteCategoryUseCase;
        
        public CategoryController(IGetAllCategoryUseCase getAllCategoryUseCase, IGetByIdCategoryUseCase getByIdCategoryUseCase, IInsertCategoryUseCase insertCategoryUseCase,
            IUpdateCategoryUseCase updateCategoryUseCase, IDeleteCategoryUseCase deleteCategoryUseCase)
        {
            this.deleteCategoryUseCase = deleteCategoryUseCase;
            this.getByIdCategoryUseCase = getByIdCategoryUseCase;
            this.getAllCategoryUseCase = getAllCategoryUseCase;
            this.insertCategoryUseCase = insertCategoryUseCase;
            this.updateCategoryUseCase = updateCategoryUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaged([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await getAllCategoryUseCase.ExecuteAsync(pageNumber, pageSize, cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await getByIdCategoryUseCase.ExecuteAsync(id, cancellationToken));
        }

        [Authorize("AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDTO category, CancellationToken cancellationToken)
        {
            await insertCategoryUseCase.ExecuteAsync(category, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] string name, CancellationToken cancellationToken)
        {

            await updateCategoryUseCase.ExecuteAsync(new CategoryDTO { Id = id, Name = name}, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            await deleteCategoryUseCase.ExecuteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
