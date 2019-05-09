using ProductAPICore.Model.Core.Repository;
using System.Threading.Tasks;

namespace ProductAPICore.Model.Core
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository Users { get; }
        IProductRepository Products { get; }
        ICompanyRepository Companies { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}