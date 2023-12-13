using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Models;
using SistemaFacturacion.Datos;
using Microsoft.AspNetCore.Authorization;

namespace SistemaFacturacion.Controllers
{
    public class RolController : Controller
    {
        // Crear una instancia de la clase RolDatos
        RolDatos rd = new RolDatos();
        // Asegurarse de que el usuario actual tiene el rol de "Administrador"
        [Authorize(Roles = "Administrador")]
        // Método para mostrar todos los roles
        public IActionResult Roles()
        {
            // Obtener todos los roles
            var list = rd.ListRoles();
            // Devolver la vista con la lista de roles
            return View(list);
        }
        // Método para mostrar la vista de creación de un nuevo rol
        public IActionResult newRol()
        {
            return View();
        }
        // Método para manejar la acción de creación de un nuevo rol
        [HttpPost]
        public IActionResult newRol(RolesModel oRol)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                return View();
            }
            // Crear el nuevo rol
            var answer = rd.newRol(oRol);
            // Si la creación fue exitosa, redirigir a la vista de roles
            // Si no, volver a mostrar la vista de creación
            if (answer)
            {
                return RedirectToAction("Roles");
            }
            else
            {
                return View();
            }
        }
        // Método para mostrar la vista de edición de un rol
        public IActionResult editRol(int IdRol)
        {
            // Obtener el rol que se va a editar
            var oRol = rd.getRoles(IdRol);
            // Obtener todos los roles
            List<RolesModel> roles = rd.ListRoles();
            // Asignar los roles a ViewBag.Roles
            ViewBag.Roles = roles;
            // Devolver la vista de edición con el rol a editar
            return View(oRol);
        }
        // Método para manejar la acción de edición de un rol
        [HttpPost]
        public IActionResult editRol(RolesModel oRol)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                return View();
            }
            // Editar el rol
            var answer = rd.editRol(oRol);
            // Si la edición fue exitosa, redirigir a la vista de roles
            // Si no, volver a mostrar la vista de edición
            if (answer)
            {
                return RedirectToAction("Roles");
            }
            else
            {
                return View();
            }
        }
        // Método para mostrar la vista de eliminación de un rol
        public IActionResult deleteRol(int IdRol)
        {
            // Obtener el rol que se va a eliminar
            var oRol = rd.getRoles(IdRol);

            // Devolver la vista de eliminación con el rol a eliminar
            return View(oRol);
        }
        // Método para manejar la acción de eliminación de un rol
        [HttpPost]
        public IActionResult deleteClient(RolesModel oRol)
        {
            // Eliminar el rol
            var answer = rd.deleteRol(oRol.IdRol);
            // Si la eliminación fue exitosa, redirigir a la vista de roles
            // Si no, volver a mostrar la vista de eliminación
            if (answer)
            {
                return RedirectToAction("Roles");
            }
            else
            {
                return View();
            }
        }
    }
}
