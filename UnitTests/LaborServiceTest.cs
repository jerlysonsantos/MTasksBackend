
using Moq;

using Application.Modules.LaborModule.Interfaces;
using Application.Modules.LaborModule.Models;
using Application.Modules.LaborModule.Services;
using Application.Modules.LaborModule.DTO;

namespace UnitTests
{
    public class LaborServiceTest
    {
        [Fact]
        public async Task Get_ReturnsLaborPaginateDTO()
        {
            int userId = 1;
            int page = 1;
            int size = 10;

            int expectedCount = 5;
            List<Labor> expectedLabors = new List<Labor>()
            {
                new Labor { Id = 1, Title = "Task 1", Description = "Description 1", IsDone = false, UserId = userId },
                new Labor { Id = 2, Title = "Task 2", Description = "Description 2", IsDone = true, UserId = userId },
            };

            var laborRepositoryMock = new Mock<ILaborRepository>();
            laborRepositoryMock.Setup(repo => repo.GetCount(userId, page, size)).ReturnsAsync(expectedCount);
            laborRepositoryMock.Setup(repo => repo.GetAll(userId, page, size)).ReturnsAsync(expectedLabors);

            var laborService = new LaborService(laborRepositoryMock.Object);


            var result = await laborService.Get(userId, page, size);

            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Size);
            Assert.Equal(expectedLabors.Count, result.Labors.Count);
        }


        [Fact]
        public async Task GetById_ReturnsLaborDTO()
        {

            int id = 1;
            int userId = 1;

            Labor labor = new Labor { Id = id, Title = "Task 1", Description = "Description 1", IsDone = false, UserId = userId };
            LaborDTO expectedLaborDTO = new LaborDTO(id, "Task 1", "Description 1", false);

            var laborRepositoryMock = new Mock<ILaborRepository>();
            laborRepositoryMock.Setup(repo => repo.GetOne(id, userId)).ReturnsAsync(labor);

            var laborService = new LaborService(laborRepositoryMock.Object);

            var result = await laborService.GetById(id, userId);

            Assert.NotNull(result);
            Assert.Equal(expectedLaborDTO.Id, result.Id);
            Assert.Equal(expectedLaborDTO.Title, result.Title);
            Assert.Equal(expectedLaborDTO.Description, result.Description);
            Assert.Equal(expectedLaborDTO.IsDone, result.IsDone);
        }


        [Fact]
        public async Task Add_ReturnsLaborDTO()
        {
            CreateLaborDTO laborDTO = new CreateLaborDTO("Task", "Description", false);

            int userId = 1;

            Labor returnLabor = new Labor
            {
                Id = 1,
                Title = "Task",
                Description = "Description",
                IsDone = false,
                UserId = userId
            };

            LaborDTO expectedLaborDTO = new LaborDTO(1, laborDTO.Title, laborDTO.Description, laborDTO.IsDone);

            var laborRepositoryMock = new Mock<ILaborRepository>();

            laborRepositoryMock.Setup(repo => repo.Add(It.IsAny<Labor>())).ReturnsAsync(returnLabor);

            var laborService = new LaborService(laborRepositoryMock.Object);

            var result = await laborService.Add(laborDTO, userId);

            Assert.NotNull(result);
            Assert.Equal(expectedLaborDTO.Id, result.Id);
            Assert.Equal(expectedLaborDTO.Title, result.Title);
            Assert.Equal(expectedLaborDTO.Description, result.Description);
            Assert.Equal(expectedLaborDTO.IsDone, result.IsDone);
        }

        [Fact]
        public async Task Update_ReturnsLaborDTO()
        {
            int userId = 1;

            Labor returnLabor = new Labor
            {
                Id = 1,
                Title = "Task",
                Description = "Description",
                IsDone = false,
                UserId = userId
            };

            LaborDTO expectedLaborDTO = new LaborDTO(1, "Task", "Description", false);

            var laborRepositoryMock = new Mock<ILaborRepository>();

            laborRepositoryMock.Setup(repo => repo.Update(It.IsAny<Labor>())).ReturnsAsync(returnLabor);

            var laborService = new LaborService(laborRepositoryMock.Object);
            int id = 1;

            UpdateLaborDTO laborDTO = new UpdateLaborDTO("Updated Task", "Updated Description", true);

            var result = await laborService.Update(id, laborDTO, userId);

            Assert.NotNull(result);
            Assert.Equal(expectedLaborDTO.Id, result.Id);
            Assert.Equal(expectedLaborDTO.Title, result.Title);
            Assert.Equal(expectedLaborDTO.Description, result.Description);
            Assert.Equal(expectedLaborDTO.IsDone, result.IsDone);
        }

        [Fact]
        public async Task Done_ReturnsLaborDTO()
        {
            int id = 1;
            int userId = 1;

            Labor returnGetLabor = new Labor
            {
                Id = 1,
                Title = "Task",
                Description = "Description",
                IsDone = false,
                UserId = userId
            };

            Labor returnLabor = new Labor
            {
                Id = 1,
                Title = "Task",
                Description = "Description",
                IsDone = true,
                UserId = userId
            };

            var laborRepositoryMock = new Mock<ILaborRepository>();
            laborRepositoryMock.Setup(repo => repo.GetOne(id, userId)).ReturnsAsync(returnGetLabor);
            laborRepositoryMock.Setup(repo => repo.Update(returnGetLabor)).ReturnsAsync(returnLabor);

            var laborService = new LaborService(laborRepositoryMock.Object);

            var result = await laborService.Done(id, userId);

            Assert.NotNull(result);
            Assert.True(result.IsDone);
        }

        [Fact]
        public async Task Delete_ReturnsLaborDTO()
        {
            int id = 1;
            int userId = 1;

            Labor returnLabor = new Labor
            {
                Id = 1,
                Title = "Task",
                Description = "Description",
                IsDone = false,
                UserId = userId
            };

            var laborRepositoryMock = new Mock<ILaborRepository>();
            laborRepositoryMock.Setup(repo => repo.Delete(id, userId)).ReturnsAsync(returnLabor);

            var laborService = new LaborService(laborRepositoryMock.Object);

            var result = await laborService.Delete(id, userId);

            Assert.NotNull(result);
        }
    }
}