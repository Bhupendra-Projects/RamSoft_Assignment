using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RamSoft_Assignment.Data;
using RamSoft_Assignment.Repositories;

namespace RamSoft_Assignment.Tests.Repositories
{
   [TestFixture]
    public class ColumnRepositoryTests
    {
        private TaskManagementDbContext _context;
        private ColumnRepository _columnRepository;

        [SetUp]
        public void Setup()
        {
            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<TaskManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new TaskManagementDbContext(options);
            _columnRepository = new ColumnRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Clean up after each test
        }

        [Test]
        public async Task AddColumnAsync_ShouldReturnTrue_WhenColumnIsAddedSuccessfully()
        {
            // Arrange
            var column = new Column { Id = 1, Name = "To Do" };

            // Act
            var result = await _columnRepository.AddColumnAsync(column);

            // Assert
            Assert.That(result, Is.True); // Verify the add operation returns true
            var columnFromDb = await _context.Columns.FindAsync(1);
            Assert.That(columnFromDb, Is.Not.Null); // Ensure column exists in the database
            Assert.That(columnFromDb.Name, Is.EqualTo("To Do")); // Check column name
        }

        [Test]
        public async void AddColumnAsync_ShouldReturnFalse_WhenSaveFails()
        {
            // Arrange
            var column = new Column { Id = 1, Name = "To Do" };

            // Simulate a failure by detaching the entity so that SaveChangesAsync doesn't persist anything
            _context.Entry(column).State = EntityState.Detached;

            // Act
            var result = await _columnRepository.AddColumnAsync(column);

            // Assert
            Assert.That(result, Is.False); // Verify the add operation returns false when save fails
        }

        [Test]
        public async void GetColumnByIdAsync_ShouldReturnColumn_WhenColumnExists()
        {
            // Arrange
            var column = new Column { Id = 1, Name = "In Progress" };
            await _context.Columns.AddAsync(column);
            await _context.SaveChangesAsync();

            // Act
            var result = await _columnRepository.GetColumnByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null); // Ensure column is retrieved
            Assert.That(result.Id, Is.EqualTo(1)); // Check column ID
            Assert.That(result.Name, Is.EqualTo("In Progress")); // Check column name
        }

        [Test]
        public async void GetColumnByIdAsync_ShouldReturnNull_WhenColumnDoesNotExist()
        {
            // Act
            var result = await _columnRepository.GetColumnByIdAsync(999); // Non-existent ID

            // Assert
            Assert.That(result, Is.Null); // Verify no column is returned
        }
    }
}
