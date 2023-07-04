using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using UserManagement.Data;
using UserManagement.Models.DbEntities;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace UserManagement.IntegrationTests
{
    public class IntegrationTestsFixture : IAsyncLifetime
    {
        private readonly MsSqlContainer _container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithCleanUp(true)
                .Build();
        private WebApplicationFactory<Program> _applicationFactory;

        public TestServer Server => _applicationFactory.Server;
        public MsSqlContainer DbContainer => _container;

        public async Task InitializeAsync()
        {
            await _container.StartAsync();
            _applicationFactory = await CreateApplicationFactory();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _applicationFactory.DisposeAsync();
            await _container.StopAsync();
        }

        private async Task<WebApplicationFactory<Program>> CreateApplicationFactory()
        {
            var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => builder.ConfigureServices((services) =>
                {
                    services.RemoveAll<DbContextOptions<MSDbContext>>();
                    services.AddDbContext<MSDbContext>(options =>
                    {
                        options.UseSqlServer(_container.GetConnectionString());
                    });
                }));

            using var scope = factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MSDbContext>();
            await context.Database.MigrateAsync();

            context.Users.AddRange(CreateDefaultUsers());
            await context.SaveChangesAsync();

            return factory;
        }

        private IList<User> CreateDefaultUsers()
        {
            return new List<User>()
            {
                new User{FirstName = "Markus", LastName = "Schmid", EMail = "markus.schmid@paso-solutions.com"},
                new User{FirstName = "Max", LastName = "Mustermann", EMail = "test1@paso-solutions.com"},
                new User{FirstName = "Georg", LastName = "Wurzelbauer", EMail = "test2@paso-solutions.com"},
                new User{FirstName = "Dieter", LastName = "Schustermann", EMail = "test3@paso-solutions.com"},
                new User{FirstName = "Hilde", LastName = "Waldtraut", EMail = "test4@paso-solutions.com"},
            };
        }
    }
}
