using RamSoft_Assignment.DTOs;
using RamSoft_Assignment.Repositories;

namespace RamSoft_Assignment.Services
{
    public class ColumnService : IColumnService
    {
        private readonly IColumnRepository _columnRepository;
        private readonly ITaskRepository _taskRepository;

        public ColumnService(IColumnRepository columnRepository, ITaskRepository taskRepository)
        {
            _columnRepository = columnRepository;
            _taskRepository = taskRepository;
        }

        public async Task<bool> AddColumnAsync(Column column)
        {
            await _columnRepository.AddColumnAsync(column);
            return true;
        }

    }
}
