﻿using Events.Application.DTO.Category;
using Events.Application.DTO.Page;
using Events.Application.Exceptions;
using Events.Application.Interfaces.UseCase.Category;
using Events.Application.Validation.Category;
using Events.Application.Validation.EventParticipant;
using Events.Application.Validation.Page;
using Events.Domain.Models;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await insertCategoryUseCase.ExecuteAsync(category, cancellationToken);

            return Ok();
        }

        [Authorize("AdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] string name, CancellationToken cancellationToken)
        {
            var categoryDTO = new CategoryDTO { Id = id, Name = name };

            await updateCategoryUseCase.ExecuteAsync(categoryDTO, cancellationToken);

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
