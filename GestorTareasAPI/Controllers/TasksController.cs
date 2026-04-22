using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestorTareasAPI.Interfaces;
using GestorTareasAPI.Models;
using GestorTareasAPI.DTO;

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
        public async Task<ActionResult<IEnumerable<DTOTasksSalida>>> GetTasks()
        {
            var result = await _tasksService.GetAllTasksAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOTasksSalida>> GetTaskById(string id)
        {
            var result = await _tasksService.GetTasksByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(DTOTasksEntrada task)
        {
            var result = await _tasksService.CreateTasksAsync(task);
            if (!result.Success && result.Error == "400")
            {
                return BadRequest();
            } else if(result.Error == "500")
            {
                return StatusCode(500);
            } else if (!result.Success)
            {
                return BadRequest(result.Error); //General por si el programa crece
            }
                return Ok();
        }
    }
}
