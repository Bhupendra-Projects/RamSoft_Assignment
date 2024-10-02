namespace RamSoft_Assignment.Services
{
    public interface IColumnService
    {
        Task<bool> AddColumnAsync(Column column);
    }
}
