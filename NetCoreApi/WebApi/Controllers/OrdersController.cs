using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDal _orderDal;

        public OrdersController(IOrderService orderService, IOrderDal orderDal)
        {
            _orderService = orderService;
            _orderDal = orderDal;
        }

        [HttpPost("addorder")]
        public async Task<IActionResult> AddOrder()
        {
            return Ok(await _orderService.AddOrder());
        }

        [HttpGet("getorders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrders();

            var groupedOrders = orders.GroupBy(x => x.OrderGroup);

            List<OrderDto> listOrderDto = new List<OrderDto>();

            var idNumber = 0;
            foreach (var group in groupedOrders)
            {

                
                var orderDto = new OrderDto
                {
                    Id=idNumber+1,
                    Count = group.Count(),
                    Orders = group.ToList(),
                    OrderName = group.Key,
                    TotalPrice = group.Sum(x => x.ProductPrice)

                };
                idNumber++;
                listOrderDto.Add(orderDto);
            }

            return Ok(listOrderDto);
        }


        
    }
}
