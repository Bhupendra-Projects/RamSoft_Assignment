using Microsoft.AspNetCore.Mvc;
using RamSoft_Assignment.DTOs;
using RamSoft_Assignment.Services;

namespace RamSoft_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly IColumnService _columnService;

        public ColumnController(IColumnService columnService)
        {
            _columnService = columnService;
        }

        // POST: api/column
        [HttpPost]
        public async Task<IActionResult> CreateColumn(ColumnDto columnDto)
        {
            if (columnDto == null || string.IsNullOrWhiteSpace(columnDto.Name))
            {
                return BadRequest("Column name is required.");
            }

            var column = new Column { Name = columnDto.Name };
           var val = await _columnService.AddColumnAsync(column);

            return Ok(val);
        }

    }
}
