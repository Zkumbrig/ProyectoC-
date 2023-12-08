using System.ComponentModel.DataAnnotations;
namespace SistemaFacturacion.Models
{
    // Modelo para los clientes
    public class ClientesModel
    {
        public int IdClient { get; set; }

        [Required(ErrorMessage ="El campo Nombres es olbigatorio")]
        public string? NameClient { get; set; }

        [Required(ErrorMessage = "El campo Apellidos es olbigatorio")]
        public string? LastNameClient { get; set; }

        [Required(ErrorMessage = "El campo Telefono es olbigatorio")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "El campo DNI es olbigatorio")]
        public string? DNI { get; set; }
    }
}
