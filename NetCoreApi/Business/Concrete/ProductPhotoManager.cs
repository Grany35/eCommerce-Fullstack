using Business.Abstract;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Aspects.Caching;
using Core.Utilities.Cloudinary;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductPhotoManager : IProductPhotoService
    {
        public IConfiguration Configuration { get; }
        private CloudinarySettings _cloudinarySettings;
        private readonly Cloudinary _cloudinary;
        private readonly IProductPhotoDal _productPhotoDal;


        public ProductPhotoManager(IProductPhotoDal productPhotoDal, IConfiguration configuration)
        {
            Configuration = configuration;
            _cloudinarySettings = Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            var account = new Account
                (
                    _cloudinarySettings.CloudName,
                    _cloudinarySettings.ApiKey,
                    _cloudinarySettings.ApiSecret
                );
            _cloudinary = new Cloudinary(account);
            _productPhotoDal = productPhotoDal;
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(550).Width(550)
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }

        [CacheAspect(60)]
        public async Task<List<ProductPhoto>> GetProductPhotos(int productId)
        {
            return new List<ProductPhoto>(await _productPhotoDal.GetAllAsnc(x => x.ProductId == productId));
        }


        public async Task<ProductPhoto> GetPhoto(int photoId)
        {
            return await _productPhotoDal.GetAsnc(x => x.Id == photoId);
        }

        [RemoveCacheAspect("IProductPhotoService.Get")]
        public async Task AddProductPhoto(IFormFile file, int productId)
        {
            var photoResult = await AddPhotoAsync(file);
            var productPhoto = new ProductPhoto
            {
                IsMain = false,
                ProductId = productId,
                PublicId = photoResult.PublicId,
                Url = photoResult.SecureUrl.AbsoluteUri
            };

            _productPhotoDal.Add(productPhoto);
        }

        [RemoveCacheAspect("IProductPhotoService.Get")]
        public async Task DeleteProductPhoto(int photoId)
        {
            var photo = await GetPhoto(photoId);

            if (photo.IsMain)
            {
                throw new ApplicationException("Birincil fotoğrafı silemezsin!");
            }

            await DeletePhotoAsync(photo.PublicId);
            _productPhotoDal.Delete(photo);
        }

        [RemoveCacheAspect("IProductPhotoService.Get")]
        public void Update(ProductPhoto productPhoto)
        {
            _productPhotoDal.Update(productPhoto);
        }
    }
}
