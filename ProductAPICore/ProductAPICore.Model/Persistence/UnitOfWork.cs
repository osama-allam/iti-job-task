using ProductAPICore.Model.Core;
using ProductAPICore.Model.Core.Repository;
using ProductAPICore.Model.Persistence.Repository;
using System.Threading.Tasks;

namespace ProductAPICore.Model.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Companies = new CompanyRepository(_context);
            Products = new ProductRepository(_context);
            Users = new ApplicationUserRepository(_context);
        }

        public IApplicationUserRepository Users { get; }
        public ICompanyRepository Companies { get; }
        public IProductRepository Products { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }


        public void Dispose() => _context.Dispose();

    }
}