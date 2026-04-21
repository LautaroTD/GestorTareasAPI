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
        

    }
}
