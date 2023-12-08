using System.ComponentModel.DataAnnotations;
namespace SistemaFacturacion.Models
{
    // Modelo para los Usuarios
    public class UsuariosModel
    {
        public int IdUser { get; set; }
        [Required(ErrorMessage = "El campo Nombres es olbigatorio")]
        public string? NombreUsuario { get; set; }
        [Required(ErrorMessage = "El campo Correo es olbigatorio")]
        public string? Correo { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es olbigatorio")]
        public string? Contraseña { get; set; }
        [Required(ErrorMessage = "El campo Rol es olbigatorio")]
        // Relacion con el modelo RolesModel
        public RolesModel Roles { get; set; }
    }
}
