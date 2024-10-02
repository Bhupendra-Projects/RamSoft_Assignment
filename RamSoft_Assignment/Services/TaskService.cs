using RamSoft_Assignment.DTOs;
using RamSoft_Assignment.Repositories;

namespace RamSoft_Assignment.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> AddTaskAsync(Task task)
        {
            await _taskRepository.AddTaskAsync(task);
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskRepository.DeleteTaskAsync(id);
        }

        public async Task<Task> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetTaskByIdAsync(id);
        }

        public async Task<bool> UpdateTaskAsync(Task task)
        {
            return await _taskRepository.UpdateTaskAsync(task);
        }
    }

}
