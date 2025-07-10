using Microsoft.AspNetCore.Mvc;
using Todo.Api.Models;
using Todo.Api.Services;

namespace Todo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
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

}
