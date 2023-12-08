using SistemaFacturacion.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaFacturacion.Datos
{
    // Definición de la clase UsuarioDatos
    public class UsuarioDatos
    {
        // Método para obtener una lista de todos los usuarios
        // Método para obtener una lista de todos los usuarios
        public List<UsuariosModel> ListUsers(string searchString = null)
        {
            // Creación de una nueva lista de usuarios
            var usuarios = new List<UsuariosModel>();
            // Establecimiento de una conexión a la base de datos
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                // Apertura de la conexión a la base de datos
                cn.Open();

                // Creación de un nuevo comando SQL para ejecutar el procedimiento almacenado "getUsers"
                SqlCommand cmd = new SqlCommand("ListUsers", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Ejecución del comando y recogida de los resultados en un SqlDataReader
                using (var dr = cmd.ExecuteReader())
                {
                    // Lectura de cada fila de los resultados
                    while (dr.Read())
                    {
                        // Creación de un nuevo objeto UsuariosModel con los datos de la fila actual
                        var usuario = new UsuariosModel
                        {
                            IdUser = Convert.ToInt32(dr["Id"]),
                            NombreUsuario = dr["NombreUsuario"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Contraseña = dr["Contraseña"].ToString(),
                            Roles = dr["Roles"].ToString()
                        };
                        // Si no se proporcionó una cadena de búsqueda, o si el Nombre del usuario contiene la cadena de búsqueda,
                        // agregar el usuario a la lista.
                        if (string.IsNullOrEmpty(searchString) || usuario.NombreUsuario.Contains(searchString))
                        {
                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            // Devolución de la lista de usuarios
            return usuarios;
        }
        // Método para validar las credenciales de un usuario
        public UsuariosModel ValidarUsuario(string _correo, string _clave)
        {
            // Inicialización de la variable usuario a null
            UsuariosModel usuario = null;

            // Establecimiento de una conexión a la base de datos
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                // Apertura de la conexión a la base de datos
                cn.Open();

                // Creación de un nuevo comando SQL para ejecutar el procedimiento almacenado "getUser"
                SqlCommand cmd = new SqlCommand("getUser", cn);

                // Adición de los parámetros al comando
                cmd.Parameters.AddWithValue("Correo", _correo);
                cmd.Parameters.AddWithValue("Contraseña", _clave);
                cmd.CommandType = CommandType.StoredProcedure;

                // Ejecución del comando y recogida de los resultados en un SqlDataReader
                using (var dr = cmd.ExecuteReader())
                {
                    // Lectura de cada fila de los resultados
                    while (dr.Read())
                    {
                        // Creación de un nuevo objeto UsuariosModel con los datos de la fila actual
                        usuario = new UsuariosModel
                        {
                            IdUser = Convert.ToInt32(dr["Id"]),
                            NombreUsuario = dr["NombreUsuario"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Contraseña = dr["Contraseña"].ToString(),
                            Roles = dr["Roles"].ToString()
                        };
                    }
                }
            }
            // Devolución del usuario si las credenciales son válidas, o null si no lo son
            return usuario;
        }
        // Método para obtener los datos de un usuario
        public UsuariosModel getUsers(int IdUser)
        {
            var oUsuario = new UsuariosModel();
            // Usar una conexión SQL
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                // Abrir la conexión 
                cn.Open();
                // Crear un nuevo comando SQL
                SqlCommand cmd = new SqlCommand("getUsers", cn);
                cmd.Parameters.AddWithValue("Id", IdUser);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    // Usar un DataReader para leer los resultados
                    while (dr.Read())
                    {
                        oUsuario.IdUser = Convert.ToInt32(dr["Id"]);
                        oUsuario.NombreUsuario = dr["NombreUsuario"].ToString();
                        oUsuario.Correo = dr["Correo"].ToString();
                        oUsuario.Contraseña = dr["Contraseña"].ToString();
                        oUsuario.Roles = dr["Roles"].ToString();
                    }
                }
            }
            // Devolver el objeto UsuariosModel con los datos del usuario
            return oUsuario;
        }
        // Metodo para agregar un nuevo usuario
        public bool newUser(UsuariosModel oUsuario)
        {
            bool rpta;
            try
            {   // Establecimiento de una conexión a la base de datos
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Apertura de la conexión a la base de datos
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("AddUser", cn);
                    cmd.Parameters.AddWithValue("NombreUsuario", oUsuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                    cmd.Parameters.AddWithValue("Contraseña", oUsuario.Contraseña);
                    cmd.Parameters.AddWithValue("Roles", oUsuario.Roles);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Ejecutar el comando para agregar el nuevo Usuario
                    cmd.ExecuteNonQuery();
                }
                // La operación fue exitosa
                rpta = true;
            }
            catch (Exception ex)
            {
                // Hubo un error durante la operación
                string error = ex.Message;
                rpta = false;
            }
            // Retornar el resultado de la operación (true o false)
            return rpta;
        }
        // Metodo para editar a un Usuario
        public bool editUser(UsuariosModel oUsuario)
        {
            bool rpta;
            try
            {   // Establecimiento de una conexión a la base de datos
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Apertura de la conexión a la base de datos
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("EditUser", cn);
                    cmd.Parameters.AddWithValue("Id", oUsuario.IdUser);
                    cmd.Parameters.AddWithValue("NombreUsuario", oUsuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                    cmd.Parameters.AddWithValue("Contraseña", oUsuario.Contraseña);
                    cmd.Parameters.AddWithValue("Roles", oUsuario.Roles);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Ejecutar el comando para editar al Usuario
                    cmd.ExecuteNonQuery();
                }
                // La operación fue exitosa
                rpta = true;
            }
            catch (Exception ex)
            {
                // Hubo un error durante la operación
                string error = ex.Message;
                rpta = false;
            }
            // Retornar el resultado de la operación (true o false)
            return rpta;
        }
        // Metodo para eliminar a un Usuario
        public bool deleteUser(int IdUser)
        {
            bool rpta;
            try
            {   // Establecimiento de una conexión a la base de datos
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Apertura de la conexión a la base de datos
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("DeleteUser", cn);
                    cmd.Parameters.AddWithValue("Id", IdUser);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Ejecutar el comando para eliminar al Usuario
                    cmd.ExecuteNonQuery();
                }
                // La operación fue exitosa
                rpta = true;
            }
            catch (Exception ex)
            {
                // Hubo un error durante la operación
                string error = ex.Message;
                rpta = false;
            }
            // Retornar el resultado de la operación (true o false)
            return rpta;
        }
    }

}
