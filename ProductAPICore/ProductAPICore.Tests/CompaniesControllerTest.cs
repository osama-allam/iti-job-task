using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ProductAPICore.API.Controllers;
using ProductAPICore.API.ViewModels;
using System.Collections.Generic;
using TestHelpers;

namespace ProductAPICore.Tests
{
    public class CompaniesControllerTest : DatabaseTestBase
    {

        #region Variables
        private CompaniesController _companiesController;
        #endregion

        #region Setup
        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _companiesController = new CompaniesController(_unitOfWork);
        }
        #endregion

        #region Unit Tests
        [Test]
        public void GetCompanies_WhenCalled_ReturnsOkResult()
        {
            //Act
            var okResult = _companiesController.GetCompanies();
            //Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void GetCompanies_WhenCalled_ReturnsListOfProducts()
        {
            //Act

            var okResult = _companiesController.GetCompanies() as OkObjectResult;
            var companiesItems = okResult.Value as List<GetCompanyViewModel>;
            //Assert
            Assert.IsInstanceOf<List<GetCompanyViewModel>>(okResult.Value);
            Assert.AreEqual(5, companiesItems.Count);
        }


        [Test]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Arrange
            var id = 200;
            // Act
            var notFoundResult = _companiesController.GetCompany(id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(notFoundResult);
        }
        [Test]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            //Arrange
            //Use this ternary operator because the UseInMemoryDatabase doesn't allow reseeding identity column and continue
            // on the last row value even if we use _dbContext.Database.EnsureDeleted(); in the DatabaseTestBase class
            var id = (testsCounter > 1) ? 6 : 1;
            // Act
            var okResult = _companiesController.GetCompany(id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            //Arrange
            //Use this ternary operator because the UseInMemoryDatabase doesn't allow reseeding identity column and continue
            // on the last row value even if we use _dbContext.Database.EnsureDeleted(); in the DatabaseTestBase class
            var id = (testsCounter > 1) ? 6 : 1;
            var okResult = _companiesController.GetCompany(id) as OkObjectResult;
            var expectedProduct = okResult.Value as GetCompanyViewModel;

            // Act
            GetCompanyViewModel actualProduct = Mapper.Map<GetCompanyViewModel>(_unitOfWork.Products.Get(id));
            // Assert
            Assert.IsInstanceOf<GetCompanyViewModel>(okResult.Value);
            Assert.AreEqual(expectedProduct.Id, actualProduct.Id);
        }
        #endregion

    }
}