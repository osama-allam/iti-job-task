using ProductAPICore.Model.Core.Domains;
using ProductAPICore.Model.Core.Repository;

namespace ProductAPICore.Model.Persistence.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {

        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
        }
    }
}