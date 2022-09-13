using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        void Add(Category category);
        void Update(Category category);
        void Delete(string categoryName);

        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(string categoryName);
    }
}
