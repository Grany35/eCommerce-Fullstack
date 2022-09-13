using Business.Abstract;
using Core.Aspects.Caching;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [RemoveCacheAspect("IBrandService.Get")]
        public void Add(Brand brand)
        {
            var brandToCheck = _brandDal.Get(x => x.BrandName.ToLower() == brand.BrandName.ToLower());

            if (brandToCheck != null)
            {
                throw new ApplicationException("Bu marka zaten mevcut!");
            }
            _brandDal.Add(brand);

        }

        [CacheAspect(30)]
        public async Task<List<Brand>> GetBrands()
        {
            return new List<Brand>(await _brandDal.GetAllAsnc());
        }
    }
}
