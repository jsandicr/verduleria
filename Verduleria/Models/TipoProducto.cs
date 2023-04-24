namespace Verduleria.Models
{
    public class TipoProducto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Producto>? Productos { get; set; }
    }
}
