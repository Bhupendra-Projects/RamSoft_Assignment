using Microsoft.EntityFrameworkCore;

namespace RamSoft_Assignment.Data
{
    public class TaskManagementDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Column> Columns { get; set; }

        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Column>()
                .HasMany<Task>() 
                .WithOne() 
                .HasForeignKey(t => t.ColumnId) 
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
