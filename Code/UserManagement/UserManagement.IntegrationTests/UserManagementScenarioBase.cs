using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.IntegrationTests
{
    public class UserManagementScenarioBase
    {
        private class UserManagementApplication: WebApplicationFactory<Program>
        {
            protected override IHost CreateHost(IHostBuilder builder)
            {
                builder.ConfigureAppConfiguration(c =>
                {
                    var directory = Path.GetDirectoryName(typeof(UserManagementApplication).Assembly.Location);
                    c.AddJsonFile(Path.Combine(directory, "appsettings.user-management.json"), false);
                });

                return base.CreateHost(builder);
            }
        }

        public TestServer CreateServer()
        {
            var factory = new UserManagementApplication();
            return factory.Server;
        }

        public static class Get
        {
            public static string Users() => "users";

            public static string UserById(int userId) => $"users/{userId}";
        }
    }
}
