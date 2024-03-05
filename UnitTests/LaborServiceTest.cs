
using Moq;

using Application.Modules.LaborModule.Interfaces;
using Application.Modules.LaborModule.Models;
using Application.Modules.LaborModule.Services;

namespace UnitTests
{
    public class LaborServiceTest
    {
        [Fact]
        public async Task Get_ReturnsLaborPaginateDTO()
        {
            // Arrange
            int userId = 1;
            int page = 1;
            int size = 10;
            int expectedCount = 5;
            List<Labor> expectedLabors = new List<Labor>()
            {
                new Labor { Id = 1, Title = "Task 1", Description = "Description 1", IsDone = false, UserId = userId },
                new Labor { Id = 2, Title = "Task 2", Description = "Description 2", IsDone = true, UserId = userId },
                // Add more sample labors as needed
            };

            var laborRepositoryMock = new Mock<ILaborRepository>();
            laborRepositoryMock.Setup(repo => repo.GetCount(userId, page, size)).ReturnsAsync(expectedCount);
            laborRepositoryMock.Setup(repo => repo.GetAll(userId, page, size)).ReturnsAsync(expectedLabors);

            var laborService = new LaborService(laborRepositoryMock.Object);

            // Act
            var result = await laborService.Get(userId, page, size);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Size);
            Assert.Equal(expectedLabors.Count, result.Labors.Count);
            // Add more assertions as needed
        }

        // Add more test methods for other methods in the LaborService class

    }
}


