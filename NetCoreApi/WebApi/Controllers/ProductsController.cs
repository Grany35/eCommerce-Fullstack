using Business.Abstract;
using Core.Extensions;
using Core.Utilities.Params;
using DataAccess.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductPhotoService _productPhotoService;

        public ProductsController(IProductService productService, IProductPhotoService productPhotoService)
        {
            _productService = productService;
            _productPhotoService = productPhotoService;
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsList([FromQuery] ProductParams productParams)
        {
            var products = await _productService.GetProductsList(productParams);

            Response.AddPaginationHeader(products.CurrentPage, products.PageSize, products.TotalCount, products.TotalPages);

            return Ok(products);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct([FromForm] ProductAddDto productAddDto)
        {
            await _productService.AddProduct(productAddDto);

            return Ok();
        }

        [HttpGet("delete-product")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
           await _productService.DeleteProduct(productId);
            return NoContent();
        }

        [HttpPost("add-photo")]
        public async Task<IActionResult> AddProductPhoto(IFormFile file, int productId)
        {
            await _productPhotoService.AddProductPhoto(file, productId);
            return NoContent();
        }

        [HttpGet("delete-photo")]
        public async Task<IActionResult> DeletePhoto(int photoId)
        {
            await _productPhotoService.DeleteProductPhoto(photoId);
            return NoContent();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductPhotos(int productId)
        {
            return Ok(await _productPhotoService.GetProductPhotos(productId));
        }

        [HttpGet("set-main-photo")]
        public async Task<IActionResult> SetMainPhoto(int photoId)
        {
           await _productService.SetMainPhoto(photoId);
            return Ok();
        }

    }
}
