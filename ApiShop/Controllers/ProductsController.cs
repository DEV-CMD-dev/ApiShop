using BusinessLogic.DTOs;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;

namespace Shop_Api_PV421.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("all")]
        public IActionResult GetAll(int? filterCategoryId)
        {
            return Ok(productsService.GetAll(filterCategoryId));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok(productsService.Get(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessages());

            var result = productsService.Create(model);

            return CreatedAtAction(
                nameof(Get),
                new { id = result.Id },
                result
            );
        }

        [HttpPut]
        public IActionResult Edit(EditProductDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessages());

            productsService.Edit(model);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            productsService.Delete(id);

            return NoContent();
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage);
        }
    }
}
