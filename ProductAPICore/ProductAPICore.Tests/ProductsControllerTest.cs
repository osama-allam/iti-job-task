using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ProductAPICore.API.Controllers;
using ProductAPICore.API.ViewModels;
using ProductAPICore.Model.Core;
using ProductAPICore.Model.Core.Domains;
using ProductAPICore.Model.Helpers;
using ProductAPICore.Model.Persistence;
using ProductAPICore.Tests.TestHelpers;
using System.Collections.Generic;

namespace ProductAPICore.Tests
{
    public class ProductsControllerTest
    {
        #region Variables
        private IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private ProductsController _productsController;
        private LinkGenerator _linkGenerator;

        public static string connectionString = "Server=.;Database=ProductAPICoreDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        #endregion

        #region Setup
        /// <summary>
        /// Initial setup for tests
        /// </summary>
        public ProductsControllerTest()
        {
            AutoMapperProfile.Configure();
        }
        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString);
            var options = builder.Options;

            _dbContext = new ApplicationDbContext(options);

            var unitOfWork = new Mock<UnitOfWork>(MockBehavior.Default, _dbContext);

            _unitOfWork = unitOfWork.Object;
            _productsController = new ProductsController(_unitOfWork, _linkGenerator);
        }
        #endregion

        #region Unit Tests

        #region Get Action

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
            var okResult = _productsController.GetProducts(productsResourceParameters);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }
        [Test]
        public void GetProducts_WhenCalled_ReturnsListOfProducts()
        {
            //Arrange
            var productsResourceParameters = new ProductsResourceParameters
            {
                PageNumber = 1,
                PageSize = 5,
                CompanyName = "",
                SearchQuery = ""
            };
            //Act
            var okResult = _productsController.GetProducts(productsResourceParameters) as OkObjectResult;
            var productItems = okResult.Value as List<GetProductViewModel>;
            //Assert
            Assert.IsInstanceOf<List<GetProductViewModel>>(okResult.Value);
            Assert.AreEqual(5, productItems.Count);
        }

        [Test]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _productsController.GetProduct(200);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(notFoundResult);
        }
        [Test]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Act
            var okResult = _productsController.GetProduct(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            //Arrange
            var okResult = _productsController.GetProduct(1) as OkObjectResult;
            var expectedProduct = okResult.Value as GetProductViewModel;

            // Act
            GetProductViewModel actualProduct = Mapper.Map<GetProductViewModel>(_unitOfWork.Products.Get(1));
            // Assert
            Assert.IsInstanceOf<GetProductViewModel>(okResult.Value);
            Assert.AreEqual(expectedProduct.Id, actualProduct.Id);
        }
        #endregion

        #region Update Action

        [Test]
        public void UpdateProduct_ExistingIdPassed_NoProductInBody_ReturnsBadRequestResult()
        {
            //Arrange
            var badRequestresult = _productsController.UpdateProduct(1, null) as BadRequestResult;

            // Act
            Product actualProduct = _unitOfWork.Products.Get(1);
            // Assert
            Assert.IsInstanceOf<BadRequestResult>(badRequestresult);
            Assert.NotNull(actualProduct);
        }
        [Test]
        public void UpdateProduct_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Arrange
            var id = 200;
            var produtBody = new UpdateProductViewModel()
            {
                Name = "Galaxy Phone",
                ImageUrl = "https://ss7.vzw.com/is/image/VerizonWireless/SAMSUNG_Galaxy_S9_Plus_Purple?$device-lg$",
                Price = 5000,
                CompanyId = 1
            };
            var notFoundResult = _productsController.UpdateProduct(id, produtBody);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(notFoundResult);
        }
        [Test]
        public void UpdateProduct_ExistingIdPassed_ReturnsNoContentResult()
        {
            //Arrange
            var id = 1;
            var produtBody = new UpdateProductViewModel()
            {
                Name = "Galaxy Phone",
                ImageUrl = "https://ss7.vzw.com/is/image/VerizonWireless/SAMSUNG_Galaxy_S9_Plus_Purple?$device-lg$",
                Price = 5000,
                CompanyId = 1
            };
            var noContentResult = _productsController.UpdateProduct(id, produtBody);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(noContentResult);
        }
        #endregion

        #endregion

    }
}
