using System.ComponentModel.DataAnnotations;

namespace GestorTareasAPI.DTO
{
    public class DTOTasksEntrada
    {
        //id de la tarea sera generado en la API (aqui)
        //datetime sera obtenido en la API (aqui)
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string IdUsuario { get; set; }

    }
}
