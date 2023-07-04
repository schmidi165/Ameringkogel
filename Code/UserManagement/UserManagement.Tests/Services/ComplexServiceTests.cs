using Microsoft.EntityFrameworkCore;
using Moq;
using UserManagement.Data;
using UserManagement.Models.Conversions;
using UserManagement.Models.DTOs;
using UserManagement.Services;
using UserManagement.Services.Impl;

namespace UserManagement.Tests.Services
{
    public class ComplexServiceTests
    {
        [Fact]
        public async Task AssignWorkItemToLeastOccupiedWorker_ThrowsExceptionAsync()
        {
            // arrange
            var userServiceMock = new Mock<IUserService>(MockBehavior.Strict);
            var workItemServiceMock = new Mock<IWorkItemService>(MockBehavior.Strict);
            var service = new ComplexService(null, userServiceMock.Object, workItemServiceMock.Object);
            var workItem = CreateWorkItem(1, "Do some work", CreateUser(1, "mschmid"));

            // act / assert
            await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await service.AssignWorkItemToLeastOccupiedWorker(workItem));
        }

        [Fact]
        public async Task AssignWorkItemToLeastOccupiedWorker_NonEqualCountAsync()
        {
            // arrange
            var users = new[]
            {
                CreateUser(1, "Markus Schmid"),
                CreateUser(2, "Max Mustermann"),
                CreateUser(3, "Frank Thelen")
            };

            var workItem = CreateWorkItem(0, "new work item");
            var workItems = new List<WorkItemDTO>
            {
                CreateWorkItem(1, "First", users[0]),
                CreateWorkItem(2, "Second", users[1]),
                CreateWorkItem(3, "Third", users[1]),
                CreateWorkItem(4, "Fourth", users[0]),
                CreateWorkItem(5, "Fifth", users[1]),
            };

            var workItemMock = new Mock<IWorkItemService>();
            workItemMock.Setup(m => m.GetAllWorkItems()).Returns(Task.FromResult<IEnumerable<WorkItemDTO>>(workItems));

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.GetUserById(0)).Returns(Task.FromResult(users[0]));

            var dbContext = CreateInMemoryDatabase(users, workItems);
            var service = new ComplexService(dbContext, userServiceMock.Object, workItemMock.Object);

            // act
            await service.AssignWorkItemToLeastOccupiedWorker(workItem);

            // assert
            userServiceMock.Verify(m => m.GetUserById(1), Times.Once);
            Assert.Equal(6, await dbContext.WorkItems.CountAsync());
        }

        [Fact]
        public async Task AssignWorkItemToLeastOccupiedWorker_TestCatchOnWorkItemServiceErrorAsync()
        {
            // arrange
            var workItemServiceMock = new Mock<IWorkItemService>(MockBehavior.Strict);
            workItemServiceMock.Setup(w => w.GetAllWorkItems()).Throws(new InvalidOperationException());
            var complexService = new ComplexService(null, null, workItemServiceMock.Object);
            var workItem = CreateWorkItem(1, "Testing");

            // act / assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await complexService.AssignWorkItemToLeastOccupiedWorker(workItem));
        }

        private WorkItemDTO CreateWorkItem(int id, string name, UserDTO user = null)
        {
            return new WorkItemDTO
            {
                Id = id,
                Description = name,
                DueDate = DateTime.UtcNow,
                EstimatedTimeEffort = TimeSpan.FromHours(1),
                PlannedStartDate = DateTime.UtcNow - TimeSpan.FromHours(1),
                TimeRemaining = TimeSpan.FromHours(1),
                Worker = user
            };
        }

        private UserDTO CreateUser(int id, string name)
        {
            return new UserDTO
            {
                Id = id,
                Fullname = name,
                Username = name
            };
        }

        private MSDbContext CreateInMemoryDatabase(IEnumerable<UserDTO> users, IEnumerable<WorkItemDTO> workItems)
        {
            var options = new DbContextOptionsBuilder<MSDbContext>()
                .UseInMemoryDatabase("UserManagement")
                .Options;

            var dbContext = new MSDbContext(options);
            dbContext.AddRange(users.Select(u => u.ToModel()));
            dbContext.SaveChanges();

            dbContext.AddRange(workItems.Select(w =>
            {
                var model = w.ToModel();
                model.User = null;
                return model;
            }));
            dbContext.SaveChanges();

            return dbContext;
        }

    }
}
