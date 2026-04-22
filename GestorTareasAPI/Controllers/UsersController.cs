using Microsoft.AspNetCore.Mvc;
using GestorTareasAPI.Models;
using GestorTareasAPI.Interfaces;

namespace GestorTareasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase //Trabajo evaluativo no pide nada de Users, borrar si asi lo desea.
    {
        private readonly GestorTareasDBContext _context;
        private readonly IUsersService _usersService; //Trabajo evaluativo no pide interfaz, borrar si asi lo desea.

        
    }
}
