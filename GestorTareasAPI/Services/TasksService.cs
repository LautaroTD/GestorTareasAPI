using GestorTareasAPI.DTO;
using GestorTareasAPI.Interfaces;
using GestorTareasAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorTareasAPI.Services
{
    public class TasksService : ITasksService //Trabajo evaluativo no pide interfaz, borrar si asi lo desea.
    {
        private readonly GestorTareasDBContext _context;
        private readonly ILogger<TasksService> _logger; //Trabajo evaluativo no pide Logger, borrar si asi lo desea.

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
            //verificacion de string
            if (string.IsNullOrWhiteSpace(id))
            {
                return new DTOTasksSalida { id = "000" }; //BadRequest (imposible que una tarea tenga esa Id porque las Ids usan GUID).
            }

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
            Result resultado = await ComprobacionInternaDeDTOTaskEntrada(task);

            if (!resultado.Success)
            {
                return resultado;
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

            //Comprobacion en loop para evitar problemas de ID repetida
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
                _logger.LogError(ex, $"Error al crear la tarea de ID:{newTask.Id}"); //Trabajo evaluativo no pide Logger, borrar si asi lo desea.
                return Result.Fail("500"); //InternalError
            }

            return Result.Ok();
        }

        public async Task<Result> UpdateTasksAsync(string id, DTOTasksEntrada task)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Result.Fail("400"); //BadRequest
            }

            Result resultado = await ComprobacionInternaDeDTOTaskEntrada(task);

            if(!resultado.Success)
            {
                return resultado;
            }

            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask is null)
            {
                return Result.Fail("500"); //InternalError (metodo accedible por UI, si puede clickar, es que existe en el sistema)
            }

            existingTask.Titulo = task.Titulo;
            existingTask.Estado = task.Estado;
            existingTask.Descripcion = task.Descripcion;
            //no permito que se cambie id, idUsuario, ni FechaDeCreacion.

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al guardar cambios de la tarea con Id: {existingTask.Id}"); //Trabajo evaluativo no pide Logger, borrar si asi lo desea.
                return Result.Fail("500"); //InternalError
            }

            return Result.Ok();
        }

        public async Task<Result> DeleteTasksAsync(string id)
        {
            //verificacion de string
            if (string.IsNullOrWhiteSpace(id))
            {
                return Result.Fail("400"); //BadRequest
            }

            var taskExistente = await _context.Tasks.FindAsync(id);

            if (taskExistente is null)
            {
                return Result.Fail("500"); //InternalError (accesible a traves de la UI, deberia solo ser posible borrar una tarea si existe).
            }

            _context.Tasks.Remove(taskExistente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la tarea de Id: {id}"); //Trabajo evaluativo no pide Logger, borrar si asi lo desea.
                return Result.Fail("500"); //InternalError 
            }

            return Result.Ok();
        }


        private async Task<Result> ComprobacionInternaDeDTOTaskEntrada(DTOTasksEntrada task)
        {
            //verificacion de string
            if (string.IsNullOrWhiteSpace(task.Titulo) || string.IsNullOrWhiteSpace(task.Descripcion) || string.IsNullOrEmpty(task.Estado))
            {
                return Result.Fail("400"); //BadRequest
            }

            //verificacion de exceso de tamaño
            if (task.Titulo.Length > 100 || task.Descripcion.Length > 300 || task.IdUsuario.Length > 300)
            {
                return Result.Fail("400"); //BadRequest
            }

            //verificacion de valores validos en status
            if (task.Estado != "pendiente" && task.Estado != "en progreso" && task.Estado != "completada")
            {
                return Result.Fail("400"); //InternalError (el estado es impuesto por la UI, deberia llegar dentro de los parametros acotados)
            }

            //verificacion de usuario valido
            if (!await _context.Users.AnyAsync(u => u.Id == task.IdUsuario))
            {
                return Result.Fail("500"); //BadRequest (la Id de usuario es un dato autoimpuesto, no depende del usuario, deberia llegar siempre a la api)
            }

            return Result.Ok();
        }


    } 
}
