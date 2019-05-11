using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ProductAPICore.API.Controllers;
using ProductAPICore.API.ViewModels;
using ProductAPICore.Model.Core;
using ProductAPICore.Model.Persistence;
using ProductAPICore.Tests.TestHelpers;
using System.Collections.Generic;

namespace ProductAPICore.Tests
{
    public class CompaniesControllerTest
    {

        #region Variables
        private IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private CompaniesController _companiesController;

        public static string connectionString = "Server=.;Database=ProductAPICoreDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        #endregion

        public CompaniesControllerTest()
        {
            AutoMapperProfile.Configure();
        }
        #region Setup
        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString);
            var options = builder.Options;

            _dbContext = new ApplicationDbContext(options);

            var unitOfWork = new Mock<UnitOfWork>(MockBehavior.Default, _dbContext);

            _unitOfWork = unitOfWork.Object;
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
            var id = 1;
            // Act
            var okResult = _companiesController.GetCompany(id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            //Arrange
            var id = 1;
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