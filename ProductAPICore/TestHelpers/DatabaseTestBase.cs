using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductAPICore.Model.Core;
using ProductAPICore.Model.Persistence;
using System;

namespace TestHelpers
{
    public class DatabaseTestBase : IDisposable
    {

        protected IUnitOfWork _unitOfWork;
        protected ApplicationDbContext _dbContext;
        protected static int testsCounter = 0;
        public DatabaseTestBase()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            _dbContext = new ApplicationDbContext(options);

            var unitOfWork = new Mock<UnitOfWork>(MockBehavior.Default, _dbContext);

            _unitOfWork = unitOfWork.Object;

            _dbContext.Database.EnsureCreated();

            _unitOfWork.EnsureSeedDataForContext();



            AutoMapperProfile.Initialize();

            testsCounter++;
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
            Mapper.Reset();
        }
    }
}