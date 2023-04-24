namespace Verduleria.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public List<DetalleCarrito>? DetalleCarrito { get; set; }
    }
}
