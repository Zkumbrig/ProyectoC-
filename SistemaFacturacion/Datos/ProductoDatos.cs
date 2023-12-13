using SistemaFacturacion.Models;
using System.Data.SqlClient;
using System.Data;

namespace SistemaFacturacion.Datos
{
    // Clase para manejar los datos de los productos
    public class ProductoDatos
    {
        // Este método devuelve una lista de productos y el número total de registros.
        public (List<ProductosModel>, int) ListProducts(string searchString = null, int pageNumber = 1, int pageSize = 15)
        {
            // Crear una nueva lista para almacenar los productos
            var oProductos = new List<ProductosModel>();
            // Conectar a la base de datos
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                cn.Open();
                // Crear un nuevo comando SQL para ejecutar el procedimiento almacenado "ListProducts"
                SqlCommand cmd = new SqlCommand("ListProduct", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                // Ejecutar el comando y procesar los resultados.
                using (var dr = cmd.ExecuteReader())
                {
                    // Leer cada fila de los resultados
                    while (dr.Read())
                    {
                        // Crear un nuevo objeto ProductosModel con los datos de la fila actual
                        var product = new ProductosModel
                        {
                            IdProduct = Convert.ToInt32(dr["IdProducto"]),
                            NameProduct = dr["NombreProducto"].ToString(),
                            Description = dr["Descripcion"].ToString(),
                            Price = dr.GetDecimal(dr.GetOrdinal("Precio")),
                        };
                        // Si no se proporcionó una cadena de búsqueda, o si el nombre del producto contiene la cadena de búsqueda,
                        // agregar el producto a la lista.
                        if (string.IsNullOrEmpty(searchString) || product.NameProduct.Contains(searchString))
                        {
                            oProductos.Add(product);
                        }
                    }
                }
            }

            // Obtener el número total de registros
            int totalRecords = oProductos.Count;

            // Saltar los registros de las páginas anteriores y tomar solo los registros de la página actual
            oProductos = oProductos.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Devolver la lista de productos y el número total de registros.
            return (oProductos, totalRecords);
        }



        // Método para obtener los datos de un producto
        public ProductosModel getProduct(int IdProduct)
        {
            var oProductos = new ProductosModel();
            // Usar una conexión SQL
            using (var cn = new SqlConnection(Connection.GetCadenaSql()))
            {
                // Abrir la conexión 
                cn.Open();
                // Crear un nuevo comando SQL
                SqlCommand cmd = new SqlCommand("GetProduct", cn);
                cmd.Parameters.AddWithValue("IdProducto", IdProduct);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    // Usar un DataReader para leer los resultados
                    while (dr.Read())
                    {
                        oProductos.IdProduct = Convert.ToInt32(dr["IdProducto"]);
                        oProductos.NameProduct = dr["NombreProducto"].ToString();
                        oProductos.Description = dr["Descripcion"].ToString();
                        oProductos.Price = dr.GetDecimal(dr.GetOrdinal("Precio"));
                    }
                }
            }
            // Devolver el objeto ProductosModel con los datos del producto
            return oProductos;
        }

        // Método para agregar los datos de un producto
        public bool newProduct(ProductosModel oProductos)
        {
            bool rpta;
            try
            {
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Abrir la conexión 
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("AddProduct", cn);
                    cmd.Parameters.AddWithValue("NombreProducto", oProductos.NameProduct);
                    cmd.Parameters.AddWithValue("Descripcion", oProductos.Description);
                    cmd.Parameters.AddWithValue("Precio", oProductos.Price);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Ejecutar el comando para agregar el nuevo producto
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
        // Método para editar los datos de un producto
        public bool editProduct(ProductosModel oProductos)
        {
            bool rpta;
            try
            {
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Abrir la conexión 
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("EditProduct", cn);
                    cmd.Parameters.AddWithValue("IdProducto", oProductos.IdProduct);
                    cmd.Parameters.AddWithValue("NombreProducto", oProductos.NameProduct);
                    cmd.Parameters.AddWithValue("Descripcion", oProductos.Description);
                    cmd.Parameters.AddWithValue("Precio", oProductos.Price);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Ejecutar el comando para editar el producto existente
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
        // Método para eliminar un producto
        public bool deleteProduct(int IdProduct)
        {
            bool rpta;
            try
            {
                using (var cn = new SqlConnection(Connection.GetCadenaSql()))
                {
                    // Abrir la conexión 
                    cn.Open();
                    // Crear un nuevo comando SQL
                    SqlCommand cmd = new SqlCommand("DeleteProduct", cn);
                    cmd.Parameters.AddWithValue("IdProducto", IdProduct);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Ejecutar el comando para eliminar al producto
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
