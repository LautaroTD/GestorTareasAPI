using GestorTareasAPI.DTO;
using GestorTareasAPI.Interfaces;
using GestorTareasAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTareasAPI.Services
{
    public class TasksService : ITasksService
    {
        private readonly GestorTareasDBContext _context;
        private readonly ILogger<TasksService> _logger;

        public TasksService(GestorTareasDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DTOTasksSalida>> GetAllTasksAsync()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return tasks.Select(t => new DTOTasksSalida
            {
                id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                IdUsuario = t.IdUsuario,
                FechaDeCreacion = t.FechaDeCreacion
            });
        }

        public async Task<DTOTasksSalida> GetTasksByIdAsync(string id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return null;
            }
            return new DTOTasksSalida
            {
                id = task.Id,
                Titulo = task.Titulo,
                Descripcion = task.Descripcion,
                Estado = task.Estado,
                IdUsuario = task.IdUsuario,
                FechaDeCreacion = task.FechaDeCreacion
            };
        }

        public async Task<Result> CreateTasksAsync(DTOTasksEntrada task)
        {
            if (string.IsNullOrWhiteSpace(task.Titulo) || string.IsNullOrWhiteSpace(task.Descripcion) || string.IsNullOrEmpty(task.Estado) || string.IsNullOrEmpty(task.IdUsuario))
            {
                return Result.Fail("400"); //BadRequest
            }

            if(task.Estado != "pendiente" && task.Estado != "en progreso" && task.Estado != "completada")
            {
                return Result.Fail("400"); //BadRequest
            }

            if(!await _context.Users.AnyAsync(u => u.Id == task.IdUsuario))
            {
                return Result.Fail("500"); //BadRequest (la Id de usuario es un dato autoimpuesto, no depende del usuario, deberia llegar siempre a la api)
            }

            bool ocupado = true;

            var newTask = new Tasks
            {
                Titulo = task.Titulo,
                Descripcion = task.Descripcion,
                Estado = task.Estado,
                IdUsuario = task.IdUsuario,
                FechaDeCreacion = DateTime.UtcNow
            };

            do
            {
                var nuevaId = Guid.NewGuid().ToString();

                var existe = await _context.Tasks.AnyAsync(t => t.Id == nuevaId);

                if (!existe)
                {
                    ocupado = false;
                    newTask.Id = nuevaId;
                }

            } while (ocupado);

            _context.Tasks.Add(newTask);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear la tarea de ID:{newTask.Id}");
                return Result.Fail("500"); //InternalError
            }

            return Result.Ok();
        }

    } //<- Nota Para si: a veces InteliCode se come los } al generar codigo.
}
