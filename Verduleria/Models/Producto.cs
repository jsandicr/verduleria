using System.ComponentModel.DataAnnotations;
namespace Verduleria.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }
        public bool Activo { get; set; }
    }
}
