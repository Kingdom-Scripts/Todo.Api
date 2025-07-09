using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Todo.Api.Models;
using Todo.Api.Services;

namespace Todo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoService _todoService;
    private readonly DbContext _dbContext; // Add DbContext as a dependency

    public TodoController(TodoService todoService, DbContext dbContext)
    {
        _todoService = todoService;
        _dbContext = dbContext; // Initialize DbContext
    }

    // GET: /todo
    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> GetAll()
    {
        var todos = await _todoService.GetAllAsync();
        return Ok(todos);
    }

    // POST: /todo
    [HttpPost]
    public async Task<IActionResult> Create(TodoItem TodoItem)
    {
        await _todoService.AddAsync(TodoItem);
        return CreatedAtAction(nameof(GetAll), new { id = TodoItem.Id }, TodoItem);
    }

    // GET: /todo/{id}
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var task = await _dbContext.Set<TodoItem>().FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    // Update: /todo/{id}
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskDto updateTaskDto)
    {
        var task = await _dbContext.Set<TodoItem>().FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        task.Title = updateTaskDto.Title;
        task.Description = updateTaskDto.Description;
        task.DateUpdated = DateTime.UtcNow;

        _dbContext.Update(task);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    // DELETE: /todo/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(Guid id)
    {
        var task = await _dbContext.Set<TodoItem>().FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        _dbContext.Set<TodoItem>().Remove(task);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}
   