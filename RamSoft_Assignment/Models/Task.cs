﻿public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public int ColumnId { get; set; } 
    public bool IsFavorited { get; set; }
}
