namespace SistemaFacturacion.Models
{
    // Modelo para los Usuarios
    public class UsuariosModel
    {
        public int IdUser { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Roles { get; set; }
    }
}
