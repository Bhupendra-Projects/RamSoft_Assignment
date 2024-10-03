using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RamSoft_Assignment.Data;
using RamSoft_Assignment.Repositories;

namespace RamSoft_Assignment.Tests.Repositories
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        private TaskManagementDbContext _context;
        private TaskRepository _taskRepository;

        [SetUp]
        public void Setup()
        {
            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<TaskManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new TaskManagementDbContext(options);
            _taskRepository = new TaskRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Clean up after each test
        }

        [Test]
        public async System.Threading.Tasks.Task AddTaskAsync_ShouldReturnTrue_WhenTaskIsAddedSuccessfully()
        {
            // Arrange
            var task = new Task { Id = 1, Name = "Task 1", Description = "Test Task", ColumnId = 1, IsFavorited = false };

            // Act
            var result = await _taskRepository.AddTaskAsync(task);

            // Assert
            Assert.That(result, Is.True);
            var taskFromDb = await _context.Tasks.FindAsync(1);
            Assert.That(taskFromDb, Is.Not.Null);
            Assert.That(taskFromDb.Name, Is.EqualTo("Task 1"));
        }

        [Test]
        public async System.Threading.Tasks.Task GetTaskByIdAsync_ShouldReturnTask_WhenTaskExists()
        {
            // Arrange
            var task = new Task { Id = 1, Name = "Task 1", Description = "Test Task", ColumnId = 1, IsFavorited = false };
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            // Act
            var result = await _taskRepository.GetTaskByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null); 
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Task 1"));
            Assert.That(result.Description, Is.EqualTo("Test Task"));
        }

        [Test]
        public async System.Threading.Tasks.Task GetTaskByIdAsync_ShouldReturnNull_WhenTaskDoesNotExist()
        {
            // Act
            var result = await _taskRepository.GetTaskByIdAsync(999); // Non-existent ID

            // Assert
            Assert.That(result,Is.Null);
        }

        [Test]
        public async System.Threading.Tasks.Task UpdateTaskAsync_ShouldReturnTrue_WhenTaskIsUpdatedSuccessfully()
        {
            // Arrange
            var task = new Task { Id = 1, Name = "Task 1", Description = "Test Task", ColumnId = 1, IsFavorited = false };
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            // Update task
            task.Name = "Updated Task 1";

            // Act
            var result = await _taskRepository.UpdateTaskAsync(task);

            // Assert
            Assert.That(result, Is.True);
            var updatedTask = await _context.Tasks.FindAsync(1);
            Assert.That(updatedTask.Name, Is.EqualTo("Updated Task 1"));
        }

        [Test]
        public async System.Threading.Tasks.Task DeleteTaskAsync_ShouldReturnTrue_WhenTaskIsDeletedSuccessfully()
        {
            // Arrange
            var task = new Task { Id = 1, Name = "Task 1", Description = "Test Task", ColumnId = 1, IsFavorited = false };
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            // Act
            var result = await _taskRepository.DeleteTaskAsync(1);

            // Assert
            Assert.That(result, Is.True);
            var deletedTask = await _context.Tasks.FindAsync(1);
            Assert.That(deletedTask, Is.Null);
        }

        [Test]
        public async System.Threading.Tasks.Task DeleteTaskAsync_ShouldReturnFalse_WhenTaskDoesNotExist()
        {
            // Act
            var result = await _taskRepository.DeleteTaskAsync(999); // Non-existent ID

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
