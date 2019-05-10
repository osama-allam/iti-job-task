using ProductAPICore.Model.Core.Domains;
using ProductAPICore.Model.Helpers;
using System.Collections.Generic;

namespace ProductAPICore.Model.Core.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsWithCompany();
        PageList<Product> GetProductsWithCompany(ProductsResourceParameters productsParams);
        Product GetProductWithCompany(object id);
    }
}
