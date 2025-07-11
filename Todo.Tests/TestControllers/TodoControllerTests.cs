using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Todo.Api.Services;
using Todo.Api.Models;
using Todo.Api.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace Todo.Tests.TestControllers
{
    public class TodoControllerTests
    {
        [Fact]
        public async Task GetAllTodos_ReturnsOkResultWithList()
        {
            //Arrange
            var mockService = new Mock<ITodoService>();
            mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<TodoItem>
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
            }
            });

            var controller = new TodoController(mockService.Object);
            var expectedCount = 2;

            //Act

            var result = await controller.GetAll();

            //Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var todos = Assert.IsType<List<TodoItem>>(ok.Value);
            Assert.Equal(expectedCount, todos.Count);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateTodo_ReturnsCreatedAtAction()
        {
            // Arrange
            var serviceMock = new Mock<ITodoService>();
            var controller = new TodoController(serviceMock.Object);
            var newTodo = new TodoItem
            {
                Id = 2,
                Title = "New Task",
                Description = "Task Description",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            // Act
            var result = await controller.Create(newTodo);


            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdTodo = Assert.IsType<TodoItem>(createdResult.Value);
            Assert.Equal(newTodo.Id, createdTodo.Id);
        }
    }
}
