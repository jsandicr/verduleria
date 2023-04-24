namespace Verduleria.Models
{
    public class DetalleCompra
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int? Costo { get; set; }
        public Compra? Compra { get; set; }
        public Producto? Producto { get; set; }
    }
}
