namespace Verduleria.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleCompra>? DetalleCompra { get; set; }
    }
}
