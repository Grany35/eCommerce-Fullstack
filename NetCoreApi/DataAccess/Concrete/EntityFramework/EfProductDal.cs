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
    public class EfProductDal : EfEntityBase<Product, ContextDb>, IProductDal
    {
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            using (var context = new ContextDb())
            {
                var query = (from product in context.Products
                             join category in context.Categories on product.CategoryId equals category.Id
                             join brand in context.Brands on product.BrandId equals brand.Id
                             select new ProductDto
                             {
                                 CategoryName = category.CategoryName,
                                 IsActive = product.IsActive,
                                 ProductPhotos = product.ProductPhotos.ToList(),
                                 MainPhotoUrl = product.ProductPhotos.FirstOrDefault(x => x.IsMain).Url,
                                 ProductPrice = product.ProductPrice,
                                 ProductName = product.ProductName,
                                 ProductStock = product.ProductStock,
                                 Id = product.Id,
                                 BrandName =brand.BrandName
 

                            }).AsNoTracking().ToListAsync();


                return await query;

            }
        }
    }
}
