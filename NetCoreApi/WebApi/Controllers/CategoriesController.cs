using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _categoryService.Add(category);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryService.GetCategories());
        }

        [HttpGet("categoryName")]
        public async Task<IActionResult> GetCategory(string categoryName)
        {
            return Ok(await _categoryService.GetCategory(categoryName));
        }

        [HttpPost("delete{categoryName}")]
        public IActionResult DeleteCategory(string categoryName)
        {
            _categoryService.Delete(categoryName);
            return NoContent();
        }

        [HttpPost("update")]
        public IActionResult UpdateCategory(Category category)
        {
            _categoryService.Update(category);
            return NoContent();
        }

    }
}
