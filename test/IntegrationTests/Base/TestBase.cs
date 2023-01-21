using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TestSupport.Helpers;
using REA.Accounting.Infrastructure.Persistence;

namespace REA.Accounting.IntegrationTests.Base
{
    public class TestBase : IDisposable
    {
        protected readonly string _connectionString;
        protected readonly EfCoreContext _dbContext;
        protected readonly DapperContext _dapperCtx;

        public TestBase()
        {
            var config = AppSettings.GetConfiguration();
            _connectionString = config.GetConnectionString("DefaultConnection");
            _dapperCtx = new DapperContext(_connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<EfCoreContext>();

            optionsBuilder.UseSqlServer(
                _connectionString,
                msSqlOptions => msSqlOptions.MigrationsAssembly(typeof(EfCoreContext).Assembly.FullName)
            )
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .UseLazyLoadingProxies();

            _dbContext = new EfCoreContext(optionsBuilder.Options);
            _dbContext.Database.ExecuteSqlRaw("EXEC dbo.usp_resetTestDb");  // This is 4 times as fast (897ms -vs- 4 seconds) as calling                        
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}