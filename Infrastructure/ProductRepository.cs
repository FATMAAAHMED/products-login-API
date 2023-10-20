using Context;
using Infrastructure;
using ProdductApplication;
using ProductDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prouctInfrastructure
{
    public class ProductRepository:Repository<Product, string> , IProductRepository
    {
        public ProductRepository(DContext context) : base(context)
        {

        }

        public Task<IEnumerable<Product>> GetAllProducts()
        {
            IEnumerable<Product> products = _context.Product;
            return Task.FromResult(products);
        }

    }
}