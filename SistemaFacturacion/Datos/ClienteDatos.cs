using SistemaFacturacion.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net;

namespace SistemaFacturacion.Datos
{
    // Clase para manejar los datos de los clientes
    public class ClienteDatos
    {
        // Este método devuelve una lista de clientes y el número total de registros.
        public (List<ClientesModel>, int) ListClients(string searchString = null, int pageNumber = 1, int pageSize = 15)
        {
            // Crear una nueva lista para almacenar los clientes
            var oClients = new List<ClientesModel>();
            // Conectar a la base de datos
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                cn.Open();
                // Crear un nuevo comando SQL para ejecutar el procedimiento almacenado "ListClients"
                SqlCommand cmd = new SqlCommand("ListClients", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                // Ejecutar el comando y procesar los resultados.
                using (var dr = cmd.ExecuteReader())
                {
                    // Leer cada fila de los resultados
                    while (dr.Read())
                    {
                        // Crear un nuevo objeto ClienteModel con los datos de la fila actual
                        var client = new ClientesModel
                        {
                            IdClient = Convert.ToInt32(dr["IdCliente"]),
                            NameClient = dr["Nombre"].ToString(),
                            LastNameClient = dr["Apellido"].ToString(),
                            Phone = dr["Telefono"].ToString(),
                            DNI = dr["DNI"].ToString(),
                        };
                        // Si no se proporcionó una cadena de búsqueda, o si el DNI del cliente contiene la cadena de búsqueda,
                        // agregar el cliente a la lista.
                        if (string.IsNullOrEmpty(searchString) || client.DNI.Contains(searchString))
                        {
                            oClients.Add(client);
                        }
                    }
                }
            }

            // Obtener el número total de registros
            int totalRecords = oClients.Count;

            // Saltar los registros de las páginas anteriores y tomar solo los registros de la página actual
            oClients = oClients.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Devolver la lista de clientes y el número total de registros.
            return (oClients, totalRecords);
        }



        // Método para obtener los datos de un cliente
        public ClientesModel getClients(int IdClient)
        {
            var oClients = new ClientesModel();
            // Usar una conexión SQL
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                // Abrir la conexión 
                cn.Open();
                // Crear un nuevo comando SQL
                SqlCommand cmd = new SqlCommand("getClient", cn);
                cmd.Parameters.AddWithValue("IdCliente", IdClient);
                cmd.CommandType= CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    // Usar un DataReader para leer los resultados
                    while (dr.Read())
                    {
                        oClients.IdClient = Convert.ToInt32(dr["IdCliente"]);
                        oClients.NameClient = dr["Nombre"].ToString();
                        oClients.LastNameClient = dr["Apellido"].ToString();
                        oClients.DNI = dr["DNI"].ToString();
                        oClients.Phone = dr["Telefono"].ToString();
                    }
                }
            }
            // Devolver el objeto ClientesModel con los datos del cliente
            return oClients;
        }

        // Método para agregar los datos de un cliente
        public bool newClient(ClientesModel oClients)
        {
            bool rpta;
            try
            {
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Abrir la conexión 
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("addClient", cn);
                    cmd.Parameters.AddWithValue("Nombre", oClients.NameClient);
                    cmd.Parameters.AddWithValue("Apellido", oClients.LastNameClient);
                    cmd.Parameters.AddWithValue("Telefono", oClients.Phone);
                    cmd.Parameters.AddWithValue("DNI", oClients.DNI);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Ejecutar el comando para agregar el nuevo cliente
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
        // Método para editar los datos de un cliente
        public bool editClient(ClientesModel oClients)
        {
            bool rpta;
            try
            {
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Abrir la conexión 
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("editClient", cn);
                    cmd.Parameters.AddWithValue("IdCliente", oClients.IdClient);
                    cmd.Parameters.AddWithValue("Nombre", oClients.NameClient);
                    cmd.Parameters.AddWithValue("Apellido", oClients.LastNameClient);
                    cmd.Parameters.AddWithValue("Telefono", oClients.Phone);
                    cmd.Parameters.AddWithValue("DNI", oClients.DNI);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Ejecutar el comando para editar el cliente existente
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
        // Método para eliminar un cliente
        public bool deleteClient(int IdClient)
        {
            bool rpta;
            try
            {
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Abrir la conexión 
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("deleteClient", cn);
                    cmd.Parameters.AddWithValue("IdCliente", IdClient);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Ejecutar el comando para eliminar al cliente
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
