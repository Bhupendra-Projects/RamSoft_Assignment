using Moq;
using NUnit.Framework;
using RamSoft_Assignment.Repositories;
using RamSoft_Assignment.Services;

namespace RamSoft_Assignment.Tests.Services
{
    [TestFixture]
    public class ColumnServiceTests
    {
        private Mock<IColumnRepository> _mockColumnRepository;
        private Mock<ITaskRepository> _mockTaskRepository;
        private ColumnService _columnService;

        [SetUp]
        public void Setup()
        {
            _mockColumnRepository = new Mock<IColumnRepository>();
            _mockTaskRepository = new Mock<ITaskRepository>();
            _columnService = new ColumnService(_mockColumnRepository.Object, _mockTaskRepository.Object);
        }

        [Test]
        public async void AddColumnAsync_ShouldReturnTrue_WhenColumnIsAddedSuccessfully()
        {
            // Arrange
            var column = new Column { Id = 1, Name = "To Do" };
            _mockColumnRepository.Setup(repo => repo.AddColumnAsync(column)).ReturnsAsync(true); // Mock the repository behavior

            // Act
            var result = await _columnService.AddColumnAsync(column);

            // Assert
            Assert.That(result, Is.True); // Check if the service returns true
            _mockColumnRepository.Verify(repo => repo.AddColumnAsync(column), Times.Once); // Verify that the repository method was called exactly once
        }

        [Test]
        public async void AddColumnAsync_ShouldCallRepositoryMethod_WhenCalled()
        {
            // Arrange
            var column = new Column { Id = 1, Name = "In Progress" };
            _mockColumnRepository.Setup(repo => repo.AddColumnAsync(It.IsAny<Column>())).ReturnsAsync(true); // Mock any column being added

            // Act
            await _columnService.AddColumnAsync(column);

            // Assert
            _mockColumnRepository.Verify(repo => repo.AddColumnAsync(It.IsAny<Column>()), Times.Once); // Verify that the repository method was called once for any column
        }

        [Test]
        public async void AddColumnAsync_ShouldReturnTrue_ForAnyValidColumn()
        {
            // Arrange
            var column = new Column { Id = 2, Name = "Done" };
            _mockColumnRepository.Setup(repo => repo.AddColumnAsync(It.IsAny<Column>())).ReturnsAsync(true); // Mock successful addition for any column

            // Act
            var result = await _columnService.AddColumnAsync(column);

            // Assert
            Assert.That(result, Is.True); // Check if the service returns true for a valid column
        }
    }
}
