using System.ComponentModel.DataAnnotations;

namespace GestorTareasAPI.DTO
{
    public class DTOTasksEntrada
    {
        //id de la tarea sera generado en la API (aqui)
        //datetime sera obtenido en la API (aqui)
        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; }
        [Required]
        [MaxLength(300)]
        public string Descripcion { get; set; }
        [Required]
        [MaxLength(50)]
        public string Estado { get; set; }
        [Required]
        [MaxLength(300)]
        public string IdUsuario { get; set; }

    }
}
