using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Models;
using SistemaFacturacion.Datos;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace SistemaFacturacion.Controllers
{
    // Controlador para las facturas 
    public class FacturaController : Controller
    {
        // Crear una nueva instancia de FacturaDatos
        FacturaDatos fd = new FacturaDatos();
        // Método para manejar la acción Index
        [Authorize(Roles = "Administrador")]
        public IActionResult Facturas()
        {
            // Obtener una lista de facturas
            var list = fd.ListFactures();
            // Devolver la vista con la lista de facturas
            return View(list);
        }

        // Método para manejar la acción newFacture
        public IActionResult newFacture()
        {
            // Devolver la vista para crear una nueva factura
            return View();
        }

        
    }
}
