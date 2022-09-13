using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;


        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }


        [HttpPost]
        public async Task<IActionResult> AddBasket(int productId)
        {
            await _basketService.AddBasket(productId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var user = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var baskets = await _basketService.GetBaskets(user);

            return Ok(baskets);
        }

        [HttpGet("delete-basket")]
        public async Task<IActionResult> DeleteProductOnBasket(int basketId)
        {
           await _basketService.DeleteProductOnBasket(basketId);
            return NoContent();
        }

    }
}
