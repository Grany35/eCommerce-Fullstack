using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Params
{
    public class ProductParams : PaginationParams
    {
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
    }
}
