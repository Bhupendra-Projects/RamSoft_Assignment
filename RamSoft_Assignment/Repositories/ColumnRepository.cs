using RamSoft_Assignment.Data;
using Microsoft.EntityFrameworkCore;

namespace RamSoft_Assignment.Repositories
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly TaskManagementDbContext _context;

        public ColumnRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddColumnAsync(Column column)
        {
            await _context.Columns.AddAsync(column);
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Column> GetColumnByIdAsync(int id)
        {
            return await _context.Columns.FindAsync(id);
        }
    }
}
