using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;
using ProductAPICore.API.Controllers;
using ProductAPICore.API.ViewModels;
using ProductAPICore.Model.Core.Domains;
using ProductAPICore.Model.Helpers;
using System.Collections.Generic;
using TestHelpers;

namespace ProductAPICore.Tests
{
    public class ProductsControllerTest : DatabaseTestBase
    {
        #region Variables
        private ProductsController _productsController;
        private LinkGenerator _linkGenerator;
        #endregion

        #region Setup
        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
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

            //Arrange
            var id = 200;
            // Act
            var notFoundResult = _productsController.GetProduct(id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(notFoundResult);
        }
        [Test]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            //Arrange
            //Use this ternary operator because the UseInMemoryDatabase doesn't allow reseeding identity column and continue
            // on the last row value even if we use _dbContext.Database.EnsureDeleted(); in the DatabaseTestBase class
            var id = (testsCounter > 1) ? 21 : 1;
            // Act
            var okResult = _productsController.GetProduct(id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            //Arrange
            //Use this ternary operator because the UseInMemoryDatabase doesn't allow reseeding identity column and continue
            // on the last row value even if we use _dbContext.Database.EnsureDeleted(); in the DatabaseTestBase class
            var id = (testsCounter > 1) ? 21 : 1;
            var okResult = _productsController.GetProduct(id) as OkObjectResult;
            var expectedProduct = okResult.Value as GetProductViewModel;

            // Act
            GetProductViewModel actualProduct = Mapper.Map<GetProductViewModel>(_unitOfWork.Products.Get(id));
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
            //Use this ternary operator because the UseInMemoryDatabase doesn't allow reseeding identity column and continue
            // on the last row value even if we use _dbContext.Database.EnsureDeleted(); in the DatabaseTestBase class
            var id = (testsCounter > 1) ? 21 : 1;
            var badRequestresult = _productsController.UpdateProduct(id, null) as BadRequestResult;

            // Act
            Product actualProduct = _unitOfWork.Products.Get(id);
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
            //Use this ternary operator because the UseInMemoryDatabase doesn't allow reseeding identity column and continue
            // on the last row value even if we use _dbContext.Database.EnsureDeleted(); in the DatabaseTestBase class
            var id = (testsCounter > 1) ? 21 : 1;
            var produtBody = new UpdateProductViewModel()
            {
                Name = "Galaxy Phone",
                ImageUrl = "https://ss7.vzw.com/is/image/VerizonWireless/SAMSUNG_Galaxy_S9_Plus_Purple?$device-lg$",
                Price = 5000,
                //Use this ternary operator because the UseInMemoryDatabase doesn't allow reseeding identity column and continue
                // on the last row value even if we use _dbContext.Database.EnsureDeleted(); in the DatabaseTestBase class
                CompanyId = (testsCounter > 1) ? 6 : 1
            };
            var noContentResult = _productsController.UpdateProduct(id, produtBody);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(noContentResult);
        }
        #endregion

        #endregion

    }
}
