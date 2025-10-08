using Microsoft.AspNetCore.Mvc;
using DataAccess.Data.Entities;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;

namespace ApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories(int pageNumber = 1)
        {
            return Ok(await categoriesService.Get(pageNumber));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            return Ok(await categoriesService.GetById(id));
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(CategoryDto category)
        {
            await categoriesService.Edit(category);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryDto category)
        {
            var item = await categoriesService.Create(category);
            return CreatedAtAction("GetCategory", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await categoriesService.Delete(id);
            return NoContent();
        }
    }
}