namespace GestorTareasAPI.DTO
{
    public class DTOTasksSalida
    {
        //IdUsuarioNavigation fue descartado por ser irrelevante para el frontend
        public string id { get; set; }
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Estado { get; set; }

        public string IdUsuario { get; set; }

        public DateTime FechaDeCreacion { get; set; }
    }
}
