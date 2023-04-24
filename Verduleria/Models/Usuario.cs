namespace Verduleria.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Ubicacion { get; set; }
        public int IdRol { get; set; }
        public Rol? Rol { get; set; }
    }
}
