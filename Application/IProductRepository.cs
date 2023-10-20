using ProdductApplication;
using ProductDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdductApplication
{
    public interface IProductRepository : IRepository< Product, string>
    {
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
