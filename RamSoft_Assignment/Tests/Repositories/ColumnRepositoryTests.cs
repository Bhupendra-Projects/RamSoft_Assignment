using Microsoft.EntityFrameworkCore;
using Moq;
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
            var options = new DbContextOptionsBuilder<TaskManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new TaskManagementDbContext(options);
            _columnRepository = new ColumnRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async System.Threading.Tasks.Task AddColumnAsync_ShouldReturnTrue_WhenColumnIsAddedSuccessfully()
        {
            // Arrange
            var column = new Column { Id = 1, Name = "To Do" };

            // Act
            var result = await _columnRepository.AddColumnAsync(column);

            // Assert
            Assert.That(result, Is.True);
            var columnFromDb = await _context.Columns.FindAsync(1);
            Assert.That(columnFromDb, Is.Not.Null);
            Assert.That(columnFromDb.Name, Is.EqualTo("To Do"));
        }

        [Test]
        public async System.Threading.Tasks.Task GetColumnByIdAsync_ShouldReturnColumn_WhenColumnExists()
        {
            // Arrange
            var column = new Column { Id = 1, Name = "In Progress" };
            await _context.Columns.AddAsync(column);
            await _context.SaveChangesAsync();

            // Act
            var result = await _columnRepository.GetColumnByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("In Progress")); 
        }

        [Test]
        public async System.Threading.Tasks.Task GetColumnByIdAsync_ShouldReturnNull_WhenColumnDoesNotExist()
        {
            // Act
            var result = await _columnRepository.GetColumnByIdAsync(999); 

            // Assert
            Assert.That(result, Is.Null); 
        }
    }
}
