using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Models
{
    // Modelo para los productos
    public class ProductosModel
    {
        public int IdProduct { get; set; }
        [Required(ErrorMessage = "El campo Nombre Producto es olbigatorio")]
        public string NameProduct { get; set; }
        [Required(ErrorMessage = "El campo Descripción es olbigatorio")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El campo Precio es olbigatorio")]
        public decimal Price { get; set; }
    }
}
