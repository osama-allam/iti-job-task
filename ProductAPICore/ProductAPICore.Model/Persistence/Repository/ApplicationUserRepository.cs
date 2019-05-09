using Microsoft.EntityFrameworkCore;
using ProductAPICore.Model.Core.Domains;
using ProductAPICore.Model.Core.Repository;

namespace ProductAPICore.Model.Persistence.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(DbContext context) : base(context)
        {
        }
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
        }
    }
}