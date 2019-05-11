using ProductAPICore.Model.Core;
using ProductAPICore.Model.Core.Repository;
using System.Threading.Tasks;

namespace ProductAPICore.Tests.TestHelpers
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public IApplicationUserRepository Users { get; }
        public IProductRepository Products { get; }
        public ICompanyRepository Companies { get; }
        public int Complete()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CompleteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}