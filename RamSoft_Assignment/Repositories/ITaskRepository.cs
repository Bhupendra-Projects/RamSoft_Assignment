using RamSoft_Assignment.DTOs;

namespace RamSoft_Assignment.Repositories
{

    public interface ITaskRepository
    {
        Task<Task> GetTaskByIdAsync(int id);
        Task<bool> AddTaskAsync(Task task);
       Task<bool> UpdateTaskAsync(Task task);
        Task<bool> DeleteTaskAsync(int id);
    }

}
