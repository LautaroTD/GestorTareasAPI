using GestorTareasAPI.Models;
using GestorTareasAPI.DTO;
using GestorTareasAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorTareasAPI.Services
{
    public class TasksService : ITasksService
    {
        private readonly GestorTareasDBContext _context;

        public TasksService(GestorTareasDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DTOTasksSalida>> GetAllTasksAsync()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return tasks.Select(t => new DTOTasksSalida
            {
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                IdUsuario = t.IdUsuario,
                FechaDeCreacion = t.FechaDeCreacion
            });
        }
    }
}
