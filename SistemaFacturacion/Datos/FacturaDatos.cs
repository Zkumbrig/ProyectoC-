using SistemaFacturacion.Models;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace SistemaFacturacion.Datos
{
    // Clase para manejar los datos de las facturas
    public class FacturaDatos
    {
        // Método para obtener una lista de facturas
        public List<FacturasModel> ListFactures()
        {
            // Crear una nueva lista de facturas
            var oListFactures = new List<FacturasModel>();
            // Usar una conexión SQL
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                // Abrir la conexión 
                cn.Open();
                // Crear un nuevo comando SQL
                SqlCommand cmd = new SqlCommand("ListFactures", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                // Usar un DataReader para leer los resultados
                using (var dr = cmd.ExecuteReader())
                {
                    // Leer todas las filas de facturas
                    while (dr.Read())
                    {
                        // Añadir cada fila a la lista de facturas
                        oListFactures.Add(new FacturasModel
                        {
                            IdFacture = Convert.ToInt32(dr["Id"]),
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            Date = dr.GetDateTime(dr.GetOrdinal("Fecha")),
                            Price = dr.GetDecimal(dr.GetOrdinal("Precio")),
                            Total = dr.GetDecimal(dr.GetOrdinal("Total")),
                            Clientes = new ClientesModel
                            {
                                NameClient = dr["Nombre"].ToString(),
                                LastNameClient = dr["Apellido"].ToString(),
                                DNI = dr["DNI"].ToString(),
                            },
                            Productos = new ProductosModel
                            {
                                NameProduct = dr["NombreProducto"].ToString(),
                            }
                        });
                    }
                }

            }
            // Devolver la lista de facturas
            return oListFactures;
        }   
    }
}
