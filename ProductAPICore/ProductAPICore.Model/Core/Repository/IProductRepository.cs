using ProductAPICore.Model.Core.Domains;
using System.Collections.Generic;

namespace ProductAPICore.Model.Core.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsWithCompany();
        Product GetProductWithCompany(object id);
    }
}
