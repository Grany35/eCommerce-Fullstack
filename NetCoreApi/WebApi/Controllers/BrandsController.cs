using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost]
        public IActionResult AddBrand(Brand brand)
        {
            _brandService.Add(brand);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            return Ok(await _brandService.GetBrands());

        }
    }
}
