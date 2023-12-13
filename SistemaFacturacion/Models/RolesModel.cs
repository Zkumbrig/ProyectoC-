using System.ComponentModel.DataAnnotations;
namespace SistemaFacturacion.Models
{
    public class RolesModel
    {
        public int IdRol { get; set; }
        [Required(ErrorMessage = "El campo Nombre Rol es olbigatorio")]
        public string NombreRol { get; set; }
    }
}
