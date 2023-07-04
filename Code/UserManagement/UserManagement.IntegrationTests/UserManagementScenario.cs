using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UserManagement.Models.DTOs;

namespace UserManagement.IntegrationTests
{
    public class UserManagementScenario : UserManagementScenarioBase
    {
        public UserManagementScenario(IntegrationTestsFixture fixtures) : base(fixtures) { }

        [Fact]
        public async void GetAllUsers_Success()
        {
            var server = GetServer();
            var response = await server.CreateClient().GetAsync(Get.Users());

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<ICollection<UserDTO>>(responseContent);
            Assert.Equal(5, users.Count());
        }

        [Fact]
        public async void GetUserById_Success()
        {
            var server = GetServer();
            var response = await server.CreateClient().GetAsync(Get.UserById(1));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void GetUserById_NotFound()
        {
            var server = GetServer();
            var response = await server.CreateClient().GetAsync(Get.UserById(78));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
