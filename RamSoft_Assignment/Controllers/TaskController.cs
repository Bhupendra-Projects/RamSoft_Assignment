using Microsoft.AspNetCore.Mvc;
using RamSoft_Assignment.DTOs;
using RamSoft_Assignment.Services;

namespace RamSoft_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // POST: api/task
        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask(TaskDto taskDto)
        {
            if (taskDto == null || taskDto.ColumnId <= 0)
            {
                return BadRequest("Valid column ID is required.");
            }

            var task = new Task
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                Deadline = taskDto.Deadline,
                ColumnId = taskDto.ColumnId,
                IsFavorited = taskDto.IsFavorited
            };

            await _taskService.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        // GET: api/task/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(new TaskDto
            {
                Name = task.Name,
                Description = task.Description,
                Deadline = task.Deadline,
                ColumnId = task.ColumnId,
                IsFavorited = task.IsFavorited
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Task task)
        {
            if (id != task.Id || !ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _taskService.UpdateTaskAsync(task);
            if (!result)
                return BadRequest("Failed to update task.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result)
                return NotFound("Task not found.");

            return NoContent();
        }
    }
}
