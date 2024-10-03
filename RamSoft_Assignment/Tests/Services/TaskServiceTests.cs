using Moq;
using NUnit.Framework;
using RamSoft_Assignment.Repositories;
using RamSoft_Assignment.Services;

namespace RamSoft_Assignment.Tests.Services
{
    [TestFixture]
    public class TaskServiceTests
    {
        private Mock<ITaskRepository> _mockTaskRepository;
        private TaskService _taskService;

        [SetUp]
        public void Setup()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _taskService = new TaskService(_mockTaskRepository.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task AddTaskAsync_ShouldReturnTrue_WhenTaskIsAddedSuccessfully()
        {
            // Arrange
            var task = new Task { Id = 1, Name = "Test Task" };
            _mockTaskRepository.Setup(repo => repo.AddTaskAsync(task)).ReturnsAsync(true); 

            // Act
            var result = await _taskService.AddTaskAsync(task);

            // Assert
            Assert.That(result, Is.True); 
            _mockTaskRepository.Verify(repo => repo.AddTaskAsync(task), Times.Once); 
        }

        [Test]
        public async System.Threading.Tasks.Task DeleteTaskAsync_ShouldReturnTrue_WhenTaskIsDeletedSuccessfully()
        {
            // Arrange
            int taskId = 1;
            _mockTaskRepository.Setup(repo => repo.DeleteTaskAsync(taskId)).ReturnsAsync(true);

            // Act
            var result = await _taskService.DeleteTaskAsync(taskId);

            // Assert
            Assert.That(result, Is.True); 
            _mockTaskRepository.Verify(repo => repo.DeleteTaskAsync(taskId), Times.Once); 
        }

        [Test]
        public async System.Threading.Tasks.Task GetTaskByIdAsync_ShouldReturnTask_WhenTaskExists()
        {
            // Arrange
            int taskId = 1;
            var task = new Task { Id = taskId, Name = "Test Task" };
            _mockTaskRepository.Setup(repo => repo.GetTaskByIdAsync(taskId)).ReturnsAsync(task);

            // Act
            var result = await _taskService.GetTaskByIdAsync(taskId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(taskId));
            Assert.That(result.Name, Is.EqualTo("Test Task"));
            _mockTaskRepository.Verify(repo => repo.GetTaskByIdAsync(taskId), Times.Once);
        }

        [Test]
        public async System.Threading.Tasks.Task UpdateTaskAsync_ShouldReturnTrue_WhenTaskIsUpdatedSuccessfully()
        {
            // Arrange
            var task = new Task { Id = 1, Name = "Updated Task" };
            _mockTaskRepository.Setup(repo => repo.UpdateTaskAsync(task)).ReturnsAsync(true); 

            // Act
            var result = await _taskService.UpdateTaskAsync(task);

            // Assert
            Assert.That(result, Is.True); 
            _mockTaskRepository.Verify(repo => repo.UpdateTaskAsync(task), Times.Once); 
        }
    }
}
