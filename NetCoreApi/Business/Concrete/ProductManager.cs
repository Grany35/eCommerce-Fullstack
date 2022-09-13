using Business.Abstract;
using Core.Aspects.Caching;
using Core.Utilities.Pagination;
using Core.Utilities.Params;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IProductPhotoDal _productPhotoDal;
        private readonly IProductPhotoService _productPhotoService;

        public ProductManager(IProductDal productDal, IProductPhotoService productPhotoService, IProductPhotoDal productPhotoDal)
        {
            _productDal = productDal;
            _productPhotoService = productPhotoService;
            _productPhotoDal = productPhotoDal;
        }

        [RemoveCacheAspect("IProductService.Get")]
        public async Task AddProduct(ProductAddDto productAddDto)
        {
            var photoResult = await _productPhotoService.AddPhotoAsync(productAddDto.Photo);
            var product = new Product
            {
                BrandId = productAddDto.BrandId,
                CategoryId = productAddDto.CategoryId,
                IsActive = true,
                ProductName = productAddDto.ProductName,
                ProductPrice = productAddDto.ProductPrice,
                ProductStock = productAddDto.ProductStock,
            };

            _productDal.Add(product);

            var photo = new ProductPhoto
            {
                Url = photoResult.SecureUrl.AbsoluteUri,
                PublicId = photoResult.PublicId,
                IsMain = true,
                ProductId = product.Id
            };
            _productPhotoDal.Add(photo);
        }


        [RemoveCacheAspect("IProductService.Get")]
        public async Task DeleteProduct(int productId)
        {
            var product = await _productDal.GetAsnc(x => x.Id == productId);
            var photos = await _productPhotoService.GetProductPhotos(productId);
            var publicIds = photos.Select(x => x.PublicId).ToList();

            foreach (var item in publicIds)
            {
                await _productPhotoService.DeletePhotoAsync(item);
            }
            foreach (var photo in photos)
            {
                _productPhotoDal.Delete(photo);
            }
            _productDal.Delete(product);

        }

        public async Task<Product> GetProduct(int productId)
        {
            return await _productDal.GetAsnc(x => x.Id == productId);
        }

        [CacheAspect(60)]
        public async Task<PagedList<ProductDto>> GetProductsList(ProductParams productParams)
        {
            var source = await _productDal.GetAllProducts();
            if (productParams.CategoryName != null)
            {
                source = source.Where(x => x.CategoryName.ToLower() == productParams.CategoryName.ToLower()).ToList();

            }

            if (productParams.BrandName != null)
            {
                source = source.Where(x => x.BrandName.ToLower() == productParams.BrandName.ToLower()).ToList();
            }

            return await PagedList<ProductDto>.CreateAsync(source.AsQueryable(), productParams.PageNumber, productParams.PageSize);
        }

        [RemoveCacheAspect("IProductService.Get")]
        public async Task SetMainPhoto(int photoId)
        {
            var photo = await _productPhotoService.GetPhoto(photoId);
            //var product = await GetProduct(photo.ProductId);
            var productPhotos = await _productPhotoService.GetProductPhotos(photo.ProductId);
            var currentMainPhoto = productPhotos.FirstOrDefault(x => x.IsMain);

            if (photo.IsMain)
            {
                throw new ApplicationException("Zaten birincil fotoğraf!");
            }
            currentMainPhoto.IsMain = false;
            _productPhotoService.Update(currentMainPhoto);

            photo.IsMain = true;
            _productPhotoService.Update(photo);



        }
    }
}
