using Todo.Api.Models;

namespace Todo.Api.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetAllAsync();
        Task AddAsync(TodoItem TodoItem);
    }

}
