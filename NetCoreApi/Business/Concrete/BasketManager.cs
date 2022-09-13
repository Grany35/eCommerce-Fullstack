using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BasketManager : IBasketService
    {
        private readonly IBasketDal _basketDal;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketManager(IBasketDal basketDal, IProductService productService,IHttpContextAccessor httpContextAccessor)
        {
            _basketDal = basketDal;
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddBasket(int productId)
        {
            var product = await _productService.GetProduct(productId);

            var user = Convert.ToInt16(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var basket = new Basket
            {
                ProductId = productId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                UserId=user
            };
            _basketDal.Add(basket);
        }

        public async Task DeleteProductOnBasket(int basketId)
        {
            var basket = await _basketDal.GetAsnc(x => x.Id == basketId);
            _basketDal.Delete(basket);
        }

        public async Task<List<BasketDto>> GetBaskets(int userId)
        {
            var baskets = await _basketDal.GetBaskets(userId);
            return baskets;
        }
    }
}
