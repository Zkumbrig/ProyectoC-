using SistemaFacturacion.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaFacturacion.Datos
{
    public class RolDatos
    {
        public List<RolesModel> ListRoles()
        {
            var oListRoles = new List<RolesModel>();
            // Usar una conexión SQL
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                // Abrir la conexión 
                cn.Open();
                // Crear un nuevo comando SQL
                SqlCommand cmd = new SqlCommand("ListRoles", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                // Usar un DataReader para leer los resultados
                using (var dr = cmd.ExecuteReader())
                {
                    
                    while (dr.Read())
                    {
                        
                        oListRoles.Add(new RolesModel
                        {
                            IdRol = Convert.ToInt32(dr["Id"]),
                            NombreRol = dr["NombreRol"].ToString()
                        });
                    }
                }

            }
            return oListRoles;
        }
        public RolesModel getRoles(int IdRol)
        {
            var oRol = new RolesModel();
            // Usar una conexión SQL
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                // Abrir la conexión 
                cn.Open();
                // Crear un nuevo comando SQL
                SqlCommand cmd = new SqlCommand("GetRol", cn);
                cmd.Parameters.AddWithValue("Id", IdRol);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    // Usar un DataReader para leer los resultados
                    while (dr.Read())
                    {
                        oRol.IdRol = Convert.ToInt32(dr["Id"]);
                        oRol.NombreRol = dr["NombreRol"].ToString();
                    }
                }
            }
            
            return oRol;
        }
        public bool newRol(RolesModel oRol)
        {
            bool rpta;
            try
            {   // Establecimiento de una conexión a la base de datos
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Apertura de la conexión a la base de datos
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("AddRol", cn);
                    cmd.Parameters.AddWithValue("NombreRol", oRol.NombreRol);
                    cmd.CommandType = CommandType.StoredProcedure;

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
        
        public bool editRol(RolesModel oRol)
        {
            bool rpta;
            try
            {   // Establecimiento de una conexión a la base de datos
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Apertura de la conexión a la base de datos
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("EditRol", cn);
                    cmd.Parameters.AddWithValue("Id", oRol.IdRol);
                    cmd.Parameters.AddWithValue("NombreRol", oRol.NombreRol);
                    cmd.CommandType = CommandType.StoredProcedure;

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

        public bool deleteRol(int IdRol)
        {
            bool rpta;
            try
            {   // Establecimiento de una conexión a la base de datos
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Apertura de la conexión a la base de datos
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("DeleteRol", cn);
                    cmd.Parameters.AddWithValue("Id", IdRol);
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
