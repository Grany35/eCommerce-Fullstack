using Core.Utilities.Pagination;
using Core.Utilities.Params;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<PagedList<ProductDto>> GetProductsList(ProductParams productParams);
        Task<Product> GetProduct(int productId);
        Task AddProduct(ProductAddDto productAddDto);
        Task DeleteProduct(int productId);
        Task SetMainPhoto(int photoId);


    }
}
