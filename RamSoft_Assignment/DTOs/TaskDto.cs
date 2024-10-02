namespace RamSoft_Assignment.DTOs
{
    public class TaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int ColumnId { get; set; } // Foreign key for the column
        public bool IsFavorited { get; set; } // Include this if you want to allow favoriting upon creation
    }
}
