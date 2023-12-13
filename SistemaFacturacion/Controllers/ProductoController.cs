using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Models;
using SistemaFacturacion.Datos;

namespace SistemaFacturacion.Controllers
{
    // Controlador para los productos
    public class ProductoController : Controller
    {
        // Crear una nueva instancia de ProductoDatos
        ProductoDatos pd = new ProductoDatos();
        // Método para manejar la acción Productos
        public IActionResult Productos(string searchString = null, int pageNumber = 1)
        {
            // Obtener una lista de productos y el número total de registros.
            var (list, totalRecords) = pd.ListProducts(searchString, pageNumber, 15);
            // Almacenar el número total de registros y el número de página en ViewBag para usarlos en la vista.
            ViewBag.TotalRecords = totalRecords;
            ViewBag.PageNumber = pageNumber;
            // Devolver la vista con la lista de productos
            return View(list);
        }
        // Método para manejar la acción newProducto
        public IActionResult newProduct()
        {
            // Devolver la vista para crear un nuevo producto
            return View();
        }
        [HttpPost]

        public IActionResult newProduct(ProductosModel oProductos)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            if (!ModelState.IsValid)
            {
                return View();
            }

            var answer = pd.newProduct(oProductos);
            if (answer)
            {
                return RedirectToAction("Productos");
            }
            else
            {
                return View();
            }
        }

        public IActionResult editProduct(int IdProduct)
        {
            var oProductos = pd.getProduct(IdProduct);
            // Devolver la vista para crear un nuevo producto
            return View(oProductos);
        }
        [HttpPost]

        public IActionResult editProduct(ProductosModel oProductos)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            if (!ModelState.IsValid)
            {
                return View();
            }

            var answer = pd.editProduct(oProductos);
            if (answer)
            {
                return RedirectToAction("Productos");
            }
            else
            {
                return View();
            }
        }
        public IActionResult deleteProduct(int IdProduct)
        {
            var oProductos = pd.getProduct(IdProduct);
            // Devolver la vista para crear un nuevo cliente
            return View(oProductos);
        }
        [HttpPost]

        public IActionResult deleteProduct(ProductosModel oProductos)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            var answer = pd.deleteProduct(oProductos.IdProduct);
            if (answer)
            {
                return RedirectToAction("Productos");
            }
            else
            {
                return View();
            }
        }

    }
}
