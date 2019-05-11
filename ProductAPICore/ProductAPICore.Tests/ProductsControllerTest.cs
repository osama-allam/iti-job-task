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
        private ApplicationDbContext _dbContext;
        private ProductsController _productsController;
        private LinkGenerator _linkGenerator;

        public static string connectionString = "Server=.;Database=ProductAPICoreDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        #endregion

        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [SetUp]
        public void Setup()
        {

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Product, GetProductViewModel>()
                    .ForMember(dest => dest.CompanyName,
                        opt => opt.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.CompanyId,
                        opt => opt.MapFrom(src => src.Company.Id));


                config.CreateMap<Product, UpdateProductViewModel>();

                config.CreateMap<UpdateProductViewModel, Product>();


                config.CreateMap<Company, GetCompanyViewModel>();
            });
            //var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString);
            var options = builder.Options;

            _dbContext = new ApplicationDbContext(options);

            var unitOfWork = new Mock<UnitOfWork>(MockBehavior.Default, _dbContext);

            _unitOfWork = unitOfWork.Object;
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
            var okResult = _productsController.GetProducts(productsResourceParameters);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

    }
}
