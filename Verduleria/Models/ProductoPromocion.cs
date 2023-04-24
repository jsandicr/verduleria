namespace Verduleria.Models
{
    public class ProductoPromocion
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdPromocion { get; set; }
        //Si esta en true quiere decir que este producto se compra, si esta en false quiere decir que este producto se regala
        public bool? ProductoCompra { get; set; }
        //Si ProductoCompra esta en true esta cantidad se toma como la cantidad que debe comprar, si esta en false se toma como la cantidad que regala
        public int Cantidad { get; set; }
        //Si ProductoCompra esta en true y tiene PorcentajeDebita hara el regalo a el precio de la cantidad de productos
        public int? PorcentajeDebita { get; set; }
        public Promocion? Promocion { get; set; }
        public Producto? Producto  { get; set; }
    }
}
