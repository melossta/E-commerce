using E_commerce.Models.Domains;
using E_commerce.Models.DTOs;
using E_commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDto)
        {
            if (string.IsNullOrWhiteSpace(categoryDto.Name))
                return BadRequest("Category name is required.");

            var newCategory = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            await _categoryService.AddCategoryAsync(newCategory);
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.CategoryId }, newCategory);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryUpdate)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }



            category.Name = categoryUpdate.Name;
            category.Description = categoryUpdate.Description;

            await _categoryService.UpdateCategoryAsync(category);

            return Ok(category);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok(new {message="Category Deleted succesfully"});
        }
    }
}
