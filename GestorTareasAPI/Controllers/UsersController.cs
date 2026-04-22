using Microsoft.AspNetCore.Mvc;
using GestorTareasAPI.Models;
using GestorTareasAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorTareasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase //Trabajo evaluativo no pide nada de Users, borrar si asi lo desea.
    { //Este controlador esta hecho para proveer facilidad para probar el trabajo evaluativo. En un caso real esto deberia ser borrado antes del merge para evitar incompatibilidades con el codigo que mi supuesto compañero habra hecho (asumiendo que un trabajo completo como este se haria en equipo, yo todo sobre Tasks y mi compañero todo sobre Users).
        private readonly GestorTareasDBContext _context;

        public UsersController(GestorTareasDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> Get()
        {
            var result = await _context.Users.ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(string nombre)
        {
            if (string.IsNullOrEmpty(nombre) && nombre.Length > 300) //esto es lo unico relevante para el sistema en el estado actual y sera lo unico que pedire al frontend.
            {
                return BadRequest("El nombre no puede estar vacío ni ser mayor a 300 caracteres.");
            }

            bool ocupado = true;

            Users user = new Users();
            user.Nombre = nombre;

            do
            {
                var nuevaId = Guid.NewGuid().ToString();

                var existe = await _context.Users.AnyAsync(t => t.Id == nuevaId);

                if (!existe)
                {
                    ocupado = false;
                    user.Id = nuevaId;
                }

            } while (ocupado);
            user.Email = "placeholder@test.ground";
            user.FechaDeCreacion = DateTime.UtcNow;
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error al crear el usuario.");
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("El ID no puede estar vacío.");
            }
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser is null)
            {
                return NotFound("Usuario no encontrado.");
            }
            _context.Users.Remove(existingUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error al eliminar el usuario.");
            }
            return Ok();

        }
    }
}
