using Microsoft.EntityFrameworkCore;
using ProductAPICore.Model.Core.Domains;
using ProductAPICore.Model.Core.Repository;
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

        public Product GetProductWithCompany(object id)
        {
            return GetProductsWithCompany()
                .FirstOrDefault(p => p.Id == (int)id);
        }
    }
}