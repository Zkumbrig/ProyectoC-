namespace SistemaFacturacion.Models
{
    // Modelo para las facturas
    public class FacturasModel
    {
        public int IdFacture { get; set; }
        public int IdClient { get; set; }
        public int IdProduct { get; set; }
        public DateTime Date { get; set; }
        public int Cantidad { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }


        // Relacion con el modelo ClientesModel
        public ClientesModel Clientes { get; set; }
        // Relacion con el modelo ProductosModel
        public ProductosModel Productos { get; set; }

    }
}
