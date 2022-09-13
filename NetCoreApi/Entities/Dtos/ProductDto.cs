using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string MainPhotoUrl { get; set; }
        public int ProductStock { get; set; }
        public decimal ProductPrice { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public ICollection<ProductPhoto> ProductPhotos { get; set; }
    }
}
