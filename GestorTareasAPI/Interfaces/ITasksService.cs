using GestorTareasAPI.DTO;
using GestorTareasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareasAPI.Interfaces
{
    public interface ITasksService //Trabajo evaluativo no pide interfaz, borrar si asi lo desea.
    {
        Task<IEnumerable<DTOTasksSalida>> GetAllTasksAsync();
        Task<DTOTasksSalida> GetTasksByIdAsync(string id);
        Task<Result> CreateTasksAsync(DTOTasksEntrada task);
        Task<Result> UpdateTasksAsync(string id, DTOTasksEntrada task);
        Task<Result> DeleteTasksAsync(string id);
    }
}
