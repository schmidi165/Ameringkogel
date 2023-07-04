using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UserManagement.Data;
using UserManagement.Models.DbEntities;

namespace UserManagement.IntegrationTests
{
    public class UserManagementScenarioBase : IClassFixture<IntegrationTestsFixture>
    {
        private readonly IntegrationTestsFixture _fixtures;

        public UserManagementScenarioBase(IntegrationTestsFixture fixtures)
        {
            _fixtures = fixtures;
        }

        public TestServer GetServer()
        {
            //var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => builder.ConfigureServices((services) =>
            //{
            //    services.RemoveAll<DbContextOptions<MSDbContext>>();
            //    services.AddDbContext<MSDbContext>(options =>
            //    {
            //        options.UseSqlServer(_fixtures.DbContainer.GetConnectionString());
            //    });
            //}));

            //using var scope = factory.Services.CreateScope();
            //var context = scope.ServiceProvider.GetRequiredService<MSDbContext>();
            //context.Database.Migrate();

            //context.Users.AddRange(CreateDefaultUsers());
            //context.SaveChanges();

            //return factory.Server;
            return _fixtures.Server;
        }

        public static class Get
        {
            public static string Users() => "users";

            public static string UserById(int userId) => $"users/{userId}";
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
