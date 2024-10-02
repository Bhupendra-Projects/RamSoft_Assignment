namespace RamSoft_Assignment.Repositories
{
    public interface IColumnRepository
    {
        Task<Column> GetColumnByIdAsync(int id);
        Task<bool> AddColumnAsync(Column column);
    }
}
