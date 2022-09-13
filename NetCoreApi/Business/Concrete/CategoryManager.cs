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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [RemoveCacheAspect("ICategoryService.Get")]
        public void Add(Category category)
        {
            var categoryName = _categoryDal.Get(x => x.CategoryName.ToLower() == category.CategoryName.ToLower());
            if (categoryName != null)
            {
                throw new ApplicationException("Bu kategori zaten mevcut!");
            }
            _categoryDal.Add(category);
        }

        [RemoveCacheAspect("ICategoryService.Get")]
        public void Delete(string categoryName)
        {
            var category = _categoryDal.Get(x => x.CategoryName.ToLower() == categoryName.ToLower());
            if (category == null)
            {
                throw new ApplicationException("Kategori bulunamadı!");
            }
            _categoryDal.Delete(category);
        }

        [CacheAspect(30)]
        public async Task<List<Category>> GetCategories()
        {
            return new List<Category>(await _categoryDal.GetAllAsnc());
        }

        
        public async Task<Category> GetCategory(string categoryName)
        {
            return await _categoryDal.GetAsnc(x => x.CategoryName.ToLower() == categoryName.ToLower());
        }

        [RemoveCacheAspect("ICategoryService.Get")]
        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }
    }
}
