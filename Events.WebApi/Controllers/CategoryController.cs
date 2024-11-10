using Events.Application.DTO.Category;
using Events.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaged([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await categoryService.GetAllAsync(pageNumber, pageSize, cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await categoryService.GetByIdAsync(id, cancellationToken));
        }

        [Authorize("AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDTO category, CancellationToken cancellationToken)
        {
            await categoryService.InsertAsync(category, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] string name, CancellationToken cancellationToken)
        {

            await categoryService.UpdateAsync(new CategoryDTO { Id = id, Name = name}, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            await categoryService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
