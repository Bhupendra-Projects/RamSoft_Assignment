using RamSoft_Assignment.DTOs;

namespace RamSoft_Assignment.Services
{
    public interface ITaskService
    {
        Task<Task> GetTaskByIdAsync(int id);
        Task<bool> AddTaskAsync(Task task);
        Task<bool> UpdateTaskAsync(Task task);
        Task<bool> DeleteTaskAsync(int id);
    }
}
