using Microsoft.EntityFrameworkCore;
using ProductAPICore.Model.Core.Domains;
using ProductAPICore.Model.Core.Repository;
using ProductAPICore.Model.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace ProductAPICore.Model.Persistence.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }


        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public IEnumerable<Product> GetProductsWithCompany()
        {
            return _entities
                .Include(p => p.Company);
        }

        public PageList<Product> GetProductsWithCompany(ProductsResourceParameters productsParams)
        {
            var productsBeforePaging = _entities
                .Include(p => p.Company)
                .OrderBy(p => p.Id)
                .ThenBy(p => p.Name).AsQueryable();

            if (!string.IsNullOrEmpty(productsParams.CompanyName))
            {
                var companyName = productsParams.CompanyName
                    .Trim()
                    .ToLowerInvariant();
                productsBeforePaging = productsBeforePaging
                    .Where(p => p.Company.Name.ToLowerInvariant() == companyName);
            }

            if (!string.IsNullOrEmpty(productsParams.SearchQuery))
            {
                var searchQuery = productsParams.SearchQuery
                    .Trim()
                    .ToLowerInvariant();
                productsBeforePaging = productsBeforePaging
                    .Where(p => p.Company.Name.ToLowerInvariant().Contains(searchQuery) ||
                                p.Name.ToLowerInvariant().Contains(searchQuery));

            }
            return PageList<Product>.Create(productsBeforePaging,
                productsParams.PageNumber, productsParams.PageSize);
        }

        public Product GetProductWithCompany(object id)
        {
            return GetProductsWithCompany()
                .FirstOrDefault(p => p.Id == (int)id);
        }
    }
}