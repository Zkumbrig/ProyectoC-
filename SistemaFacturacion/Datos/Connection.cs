using System.Data.SqlClient;

namespace SistemaFacturacion.Datos
{
    public class Connection
    {
        // Cadena de conexion a la base de datos SQL
        private static string cadenaSql;

        // Constructor estático para inicializar la cadena de conexión
        static Connection()
        {
            // Crear un nuevo constructor de configuración
            var appset = new ConfigurationBuilder()
                // Establecer la ruta base del directorio actual
                .SetBasePath(Directory.GetCurrentDirectory())
                // Añadir al archivo de configuración appsettings.json
                .AddJsonFile("appsettings.json")
                // Construir la configuración
                .Build();
            // Obtener la cadena de conexión de la configuración
            cadenaSql = appset.GetSection("ConnectionStrings:cnx").Value;
        }

        // Método para obtener la cadena de conexión
        public static string GetCadenaSql()
        {
            // Devolver la cadena de Conexión
            return cadenaSql;
        }
    }
}
