using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Services;

namespace Todo.Tests.TestServices
{
    public class TodoServiceTests
    {
        private TodoDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new TodoDbContext(options);
        }

        private List<TodoItem> GetSeedTodos() => new()
        {
            new TodoItem
            {
                Id = 1,
                Title = "Buy groceries",
                Description = "Milk, Bread, Eggs",
                DateCreated = DateTime.UtcNow.AddDays(2),
                DateUpdated = DateTime.UtcNow.AddDays(5)
            },
            new TodoItem
            {
                Id = 2,
                Title = "Finish project",
                Description = "Complete API task",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddDays(6)
            },
            new TodoItem
            {
                Id = 3,
                Title = "Finish project",
                Description = "Complete API task",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddDays(2)
            },
            new TodoItem
            {
                Id = 4,
                Title = "Finish project",
                Description = "Complete API task",
                DateCreated = DateTime.UtcNow.AddDays(4),
                DateUpdated = DateTime.UtcNow.AddDays(8)
            }
        };

        private async Task SeedTestData(TodoService service)
        {
            foreach (var todo in GetSeedTodos())
            {
                await service.AddAsync(todo);
            }
        }

        [Fact]
        public async Task GetAllTodos_ReturnsListOfAllTodos()
        {
            //Arrange
            var context = GetDbContext();
            var service = new TodoService(context);
            var expectedCount = 4;

            await SeedTestData(service);

            //Act
            var result = await service.GetAllAsync();

            //Assert
            Assert.Equal(expectedCount, result.Count);
            Assert.True(result.Any());
            Assert.IsType<List<TodoItem>>(result);
            Assert.NotNull(result);
        }
    }
}
