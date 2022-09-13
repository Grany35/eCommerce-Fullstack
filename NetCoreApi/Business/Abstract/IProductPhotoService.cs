using CloudinaryDotNet.Actions;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<List<ProductPhoto>> GetProductPhotos(int productId);
        Task<ProductPhoto> GetPhoto(int photoId);
        Task AddProductPhoto(IFormFile file, int productId);
        Task DeleteProductPhoto(int photoId);
        void Update(ProductPhoto productPhoto);
        
    }
}
