using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Models;

namespace Todo.Api.Services;

public class TodoService
{
    private readonly TodoDbContext _context;

    public TodoService(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<List<TodoItem>> GetAllAsync() => await _context.TodoItems.ToListAsync();

    public async Task AddAsync(TodoItem TodoItem)
    {
        _context.TodoItems.Add(TodoItem);
        await _context.SaveChangesAsync();
    }

}
