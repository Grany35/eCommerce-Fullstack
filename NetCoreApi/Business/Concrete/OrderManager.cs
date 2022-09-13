using Business.Abstract;
using CloudinaryDotNet.Actions;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _http;
        public OrderManager(IOrderDal orderDal, IBasketService basketService, IHttpContextAccessor http)
        {
            _orderDal = orderDal;
            _basketService = basketService;
            _http = http;
        }

        public async Task<List<Order>> AddOrder()
        {
            var userId = Convert.ToInt16(_http.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var baskets = await _basketService.GetBaskets(userId);

            List<Order> orders = new List<Order>();
            string guid = Guid.NewGuid().ToString();

            foreach (var basket in baskets)
            {
                var order = new Order
                {
                    ProductId = basket.ProductId,
                    ProductName = basket.ProductName,
                    ProductPrice = basket.ProductPrice,
                    UserId = userId,
                    OrderGroup = guid
                };
                orders.Add(order);
                _orderDal.Add(order);
                await _basketService.DeleteProductOnBasket(basket.Id);
            }
            return orders;
        }

        public async Task<List<Order>> GetOrders()
        {
            return new List<Order>(await _orderDal.GetAllAsnc());
        }
    }
}
