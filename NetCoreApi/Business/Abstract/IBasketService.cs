using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBasketService
    {
        Task AddBasket(int productId);
        Task<List<BasketDto>> GetBaskets(int userId);
        Task DeleteProductOnBasket(int basketId);
    }
}
