using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenemesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductDal _productDal;
        private readonly IProductPhotoService _productPhotoService;

        public DenemesController(ICategoryService categoryService, IProductDal productDal, IProductPhotoService productPhotoService)
        {
            _categoryService = categoryService;
            _productDal = productDal;
            _productPhotoService = productPhotoService;
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            _categoryService.Add(category);
            return Ok();
        }


        //[HttpGet("delete-photo")]
        //public async Task<IActionResult> DeletePhoto(string publicId)
        //{
        //    _
        //    return Ok(asd);
        //}
    }
}
