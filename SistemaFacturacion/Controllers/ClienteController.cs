using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Models;
using SistemaFacturacion.Datos;

namespace SistemaFacturacion.Controllers
{
    // Controlador para los clientes
    public class ClienteController : Controller
    {
        // Crear una nueva instancia de ClienteDatos
        ClienteDatos cd = new ClienteDatos();
        // Método para manejar la acción Clientes
        public IActionResult Clientes(string searchString = null, int pageNumber = 1)
        {
            // Obtener una lista de clientes y el número total de registros.
            var (list, totalRecords) = cd.ListClients(searchString, pageNumber, 15);
            // Almacenar el número total de registros y el número de página en ViewBag para usarlos en la vista.
            ViewBag.TotalRecords = totalRecords;
            ViewBag.PageNumber = pageNumber;
            // Devolver la vista con la lista de clientes
            return View(list);
        }

        // Método para manejar la acción newClient
        public IActionResult newClient()
        {
            // Devolver la vista para crear un nuevo cliente
            return View();
        }
        [HttpPost]

        public IActionResult newClient(ClientesModel oClients)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            if (!ModelState.IsValid)
            {
                return View();
            }

            var answer = cd.newClient(oClients);
            if (answer)
            {
                return RedirectToAction("Clientes");
            }
            else 
            {
                return View();
            }
        }

        public IActionResult editClient(int IdClient)
        {
            var oClients = cd.getClients(IdClient);
            // Devolver la vista para crear un nuevo cliente
            return View(oClients);
        }
        [HttpPost]

        public IActionResult editClient(ClientesModel oClients)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            if (!ModelState.IsValid)
            {
                return View();
            }

            var answer = cd.editClient(oClients);
            if (answer)
            {
                return RedirectToAction("Clientes");
            }
            else
            {
                return View();
            }
        }
        public IActionResult deleteClient(int IdClient)
        {
            var oClients = cd.getClients(IdClient);
            // Devolver la vista para crear un nuevo cliente
            return View(oClients);
        }
        [HttpPost]

        public IActionResult deleteClient(ClientesModel oClients)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            var answer = cd.deleteClient(oClients.IdClient);
            if (answer)
            {
                return RedirectToAction("Clientes");
            }
            else
            {
                return View();
            }
        }
    }
}
