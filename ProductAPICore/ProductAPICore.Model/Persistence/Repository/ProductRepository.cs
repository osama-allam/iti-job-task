using ProductAPICore.Model.Core.Domains;
using ProductAPICore.Model.Core.Repository;

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
    }
}