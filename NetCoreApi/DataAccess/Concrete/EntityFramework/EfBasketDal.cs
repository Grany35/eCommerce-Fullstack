using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDal : EfEntityBase<Basket, ContextDb>, IBasketDal
    {
        public async Task<List<BasketDto>> GetBaskets(int userId)
        {
            using (var context = new ContextDb())
            {
                var result = (from basket in context.Baskets.Where(x => x.UserId == userId)
                             join product in context.Products on basket.ProductId equals product.Id
                             select new BasketDto
                             {
                                 Id = basket.Id,
                                 UserId = userId,
                                 MainPhotoUrl = product.ProductPhotos.FirstOrDefault(x => x.IsMain).Url,
                                 ProductId = basket.ProductId,
                                 ProductName = basket.ProductName,
                                 ProductPrice = basket.ProductPrice

                             }).ToListAsync();
                return await result;
            }
        }
    }
}
