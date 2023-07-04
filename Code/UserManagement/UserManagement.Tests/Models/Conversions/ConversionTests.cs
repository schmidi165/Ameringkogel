using Shouldly;
using UserManagement.Models.Conversions;
using UserManagement.Models.DbEntities;
using UserManagement.Models.DTOs;

namespace UserManagement.Tests.Models.Conversions
{
    public class ConversionTests
    {
        [Fact]
        public void TestValidMapperConfiguration()
        {
            var mapper = Conversion.Mapper;
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void TestSimpleUserConversion()
        {
            var mapper = Conversion.Mapper;

            var user = new User
            {
                Id = 1,
                FirstName = "Markus",
                LastName = "Schmid",
                EMail = "markus.schmid@paso-solutions.com",
            };

            var userDto = mapper.Map<UserDTO>(user);

            Assert.Equal(1, userDto.Id);
            Assert.Equal("Markus Schmid", userDto.Fullname);
            Assert.Equal(user.EMail, userDto.Username);
        }

        [Fact]
        public void TestSimpleUserDTOConversion()
        {
            var mapper = Conversion.Mapper;

            var userDto = new UserDTO
            {
                Id = 1,
                Fullname = "Markus Schmid",
                Username = "markus.schmid@paso-solutions.com"
            };

            var user = mapper.Map<User>(userDto);

            Assert.Equal(1, user.Id);
            Assert.Equal("Markus", user.FirstName);
            Assert.Equal("Schmid", user.LastName);
            Assert.Equal(userDto.Username, user.EMail);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(2, 2, 4)]
        public void TestingMaths(int a, int b, int expected)
        {
            var result = a + b;

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("First", "Last", "First Last")]
        [InlineData("Last", "First", "LastFirst")]
        public void TesingConcat(string a, string b, string expected)
        {
            var result = $"{a} {b}";
            result.ShouldBe(expected);
        }

        [Fact]
        public void TestAnotherFailed()
        {
            var result = "FirstLast";
            var expected = "First Last";

            Assert.Equal(expected, result);
        }
    }
}
