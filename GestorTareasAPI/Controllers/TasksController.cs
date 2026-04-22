using GestorTareasAPI.DTO;
using GestorTareasAPI.Interfaces;
using GestorTareasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {

        private readonly GestorTareasDBContext _context;
        private readonly ITasksService _tasksService;

        public TasksController(GestorTareasDBContext context, ITasksService tasksService)
        {
            _context = context;
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOTasksSalida>>> GetTasks([FromQuery] string? status = null)
        {
            if(status != "completada" && status != "en progreso" && status != "pendiente")
            {
                status = null;
            }

            var result = await _tasksService.GetAllTasksAsync();
            
            if (!string.IsNullOrEmpty(status))
            {
                result = result.Where(t => t.Estado == status);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOTasksSalida>> GetTaskById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var result = await _tasksService.GetTasksByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            } else if(result.id == "000")
            {
                return BadRequest();
            }
                return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(DTOTasksEntrada task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _tasksService.CreateTasksAsync(task);
            if (!result.Success && result.Error == "400")
            {
                return BadRequest();
            } else if (result.Error == "500")
            {
                return StatusCode(500);
            } else if (!result.Success)
            {
                return BadRequest(result.Error); //General por si el programa crece
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(string id, DTOTasksEntrada task)
        {
            var result = await _tasksService.UpdateTasksAsync(id, task);

            if (!result.Success && result.Error == "400")
            {
                return BadRequest();
            }
            else if (result.Error == "500")
            {
                return StatusCode(500);
            }
            else if (!result.Success)
            {
                return BadRequest(result.Error); //General por si el programa crece
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var result = await _tasksService.DeleteTasksAsync(id);

            if (!result.Success && result.Error == "400")
            {
                return BadRequest();
            }
            else if (result.Error == "500")
            {
                return StatusCode(500);
            }
            else if (!result.Success)
            {
                return BadRequest(result.Error); //General por si el programa crece
            }

            return Ok();
        }


    }
}
