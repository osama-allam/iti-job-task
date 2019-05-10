using Moq;
using NUnit.Framework;
using ProductAPICore.API.ViewModels;
using ProductAPICore.Model.Core;
using ProductAPICore.Model.Persistence;
using ProductAPICore.Model.Persistence.Repository;
using ProductAPICore.Tests.TestHelpers;
using System.Collections.Generic;

namespace ProductAPICore.Tests
{
    public class ProductsControllerTest
    {
        /*
            - ProductService = ProductRepository
            - WebApiEntities = ApplicationDbContext
            - GenericRepository = Repository
        */

        #region Variables
        private IUnitOfWork _unitOfWork;
        private List<GetProductViewModel> _products;
        private ProductRepository _productRepository;
        private ApplicationDbContext _dbContext;
        #endregion

        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [SetUp]
        public void Setup()
        {

            _products = SetUpProducts();
            _dbContext = new Mock<ApplicationDbContext>().Object;
            //_productRepository = SetUpProductRepository();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.Products).Returns(_productRepository);
            _unitOfWork = unitOfWork.Object;
            _productRepository = new ProductRepository(_dbContext);

            _products = SetUpProducts();

        }
        [Test]
        public void GetProducts_WhenCalled_ReturnsOkResult()
        {
        }
        /// <summary>
        /// Setup dummy products data
        /// </summary>
        /// <returns></returns>
        private static List<GetProductViewModel> SetUpProducts()
        {
            var prodId = new int();
            var products = DataInitializer.GetAllProducts();
            foreach (GetProductViewModel prod in products)
                prod.Id = ++prodId;
            return products;

        }
    }
}
