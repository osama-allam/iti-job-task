using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;
using ProductAPICore.API.Controllers;
using ProductAPICore.Model.Core;
using ProductAPICore.Model.Helpers;
using ProductAPICore.Tests.TestHelpers;

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
        private ProductsController _productsController;
        private LinkGenerator _linkGenerator;
        #endregion

        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _unitOfWork = new FakeUnitOfWork();
            _productsController = new ProductsController(_unitOfWork, _linkGenerator);
        }
        [Test]
        public void GetProducts_WhenCalled_ReturnsOkResult()
        {
            var productsResourceParameters = new ProductsResourceParameters
            {
                PageNumber = 1,
                PageSize = 3,
                CompanyName = "",
                SearchQuery = ""
            };
            var productController = new ProductsController(_unitOfWork, _linkGenerator);
            var okResult = productController.GetProducts(productsResourceParameters);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

    }
}
