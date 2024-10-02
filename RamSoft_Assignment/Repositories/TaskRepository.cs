using RamSoft_Assignment.Data;
using Microsoft.EntityFrameworkCore;

namespace RamSoft_Assignment.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDbContext _context;

        public TaskRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddTaskAsync(Task task)
        {
            await _context.Tasks.AddAsync(task);
          return  await _context.SaveChangesAsync() > 0;
        }

        public async Task<Task> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks
          .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> UpdateTaskAsync(Task task)
        {
            _context.Tasks.Update(task);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await GetTaskByIdAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                return await _context.SaveChangesAsync() > 0;
            }
            return false; 
        }
    }
}
