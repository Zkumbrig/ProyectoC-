using SistemaFacturacion.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaFacturacion.Datos
{
    public class RolDatos
    {
        // Método para obtener todos los roles
        public List<RolesModel> ListRoles()
        {
            // Crear una lista vacía para almacenar los roles
            var oListRoles = new List<RolesModel>();
            // Establecer una conexión a la base de datos
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                // Abrir la conexión a la base de datos
                cn.Open();
                // Crear un nuevo comando SQL para ejecutar el procedimiento almacenado "ListRoles"
                SqlCommand cmd = new SqlCommand("ListRoles", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Ejecutar el comando y obtener los resultados
                using (var dr = cmd.ExecuteReader())
                {
                    // Leer cada fila de los resultados
                    while (dr.Read())
                    {
                        // Crear un nuevo objeto RolesModel con los datos de la fila actual
                        oListRoles.Add(new RolesModel
                        {
                            IdRol = Convert.ToInt32(dr["Id"]),
                            NombreRol = dr["NombreRol"].ToString()
                        });
                    }
                }
            }
            // Devolver la lista de roles
            return oListRoles;
        }
        // Método para obtener un rol específico
        public RolesModel getRoles(int IdRol)
        {
            // Crear un nuevo objeto RolesModel para almacenar el rol
            var oRol = new RolesModel();
            // Establecer una conexión a la base de datos
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                // Abrir la conexión a la base de datos
                cn.Open();
                // Crear un nuevo comando SQL para ejecutar el procedimiento almacenado "GetRol"
                SqlCommand cmd = new SqlCommand("GetRol", cn);
                cmd.Parameters.AddWithValue("Id", IdRol);
                cmd.CommandType = CommandType.StoredProcedure;
                // Ejecutar el comando y obtener los resultados
                using (var dr = cmd.ExecuteReader())
                {
                    // Leer la fila de los resultados
                    while (dr.Read())
                    {
                        // Asignar los datos de la fila al objeto RolesModel
                        oRol.IdRol = Convert.ToInt32(dr["Id"]);
                        oRol.NombreRol = dr["NombreRol"].ToString();
                    }
                }
            }

            // Devolver el rol
            return oRol;
        }

        // Método para agregar un nuevo rol
        public bool newRol(RolesModel oRol)
        {
            // Variable para almacenar el resultado de la operación
            bool rpta;
            try
            {
                // Establecer una conexión a la base de datos
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Abrir la conexión a la base de datos
                    cn.Open();
                    // Crear un nuevo comando SQL para ejecutar el procedimiento almacenado "AddRol"
                    SqlCommand cmd = new SqlCommand("AddRol", cn);
                    cmd.Parameters.AddWithValue("NombreRol", oRol.NombreRol);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecutar el comando
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
            // Devolver el resultado de la operación
            return rpta;
        }
        // Método para editar un rol
        public bool editRol(RolesModel oRol)
        {
            // Variable para almacenar el resultado de la operación
            bool rpta;
            try
            {
                // Establecer una conexión a la base de datos
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Abrir la conexión a la base de datos
                    cn.Open();
                    // Crear un nuevo comando SQL para ejecutar el procedimiento almacenado "EditRol"
                    SqlCommand cmd = new SqlCommand("EditRol", cn);
                    cmd.Parameters.AddWithValue("Id", oRol.IdRol);
                    cmd.Parameters.AddWithValue("NombreRol", oRol.NombreRol);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Ejecutar el comando
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
            // Devolver el resultado de la operación
            return rpta;
        }
        // Método para eliminar un rol
        public bool deleteRol(int IdRol)
        {
            // Variable para almacenar el resultado de la operación
            bool rpta;
            try
            {
                // Establecer una conexión a la base de datos
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Abrir la conexión a la base de datos
                    cn.Open();
                    // Crear un nuevo comando SQL para ejecutar el procedimiento almacenado "DeleteRol"
                    SqlCommand cmd = new SqlCommand("DeleteRol", cn);
                    cmd.Parameters.AddWithValue("Id", IdRol);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Ejecutar el comando para eliminar el rol
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
            // Devolver el resultado de la operación
            return rpta;
        }
    }
}
