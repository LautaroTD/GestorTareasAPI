using GestorTareasAPI.DTO;

namespace GestorTareasAPI.Interfaces
{
    public interface ITasksService
    {
        Task<IEnumerable<DTOTasksSalida>> GetAllTasksAsync();
    }
}
