using Microsoft.EntityFrameworkCore;
using ProductAPICore.Model.Core.Domains;
using ProductAPICore.Model.Core.Repository;
using ProductAPICore.Model.Helpers;
using ProductAPICore.Model.Persistence.Repository;
using System.Collections.Generic;

namespace ProductAPICore.Tests.TestHelpers
{
    public class FakeProductRepository : Repository<Product>, IProductRepository
    {
        public FakeProductRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetProductsWithCompany()
        {
            throw new System.NotImplementedException();
        }

        public PageList<Product> GetProductsWithCompany(ProductsResourceParameters productsParams)
        {
            throw new System.NotImplementedException();
        }

        public Product GetProductWithCompany(object id)
        {
            throw new System.NotImplementedException();
        }
    }
}