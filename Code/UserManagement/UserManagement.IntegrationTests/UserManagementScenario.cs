using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.IntegrationTests
{
    public class UserScenario: UserManagementScenarioBase
    {
        [Fact]
        public async void GetAllUsers_Success()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().GetAsync(Get.Users());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void GetUserById_Success()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().GetAsync(Get.UserById(1));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void GetUserById_NotFound()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().GetAsync(Get.UserById(78));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
