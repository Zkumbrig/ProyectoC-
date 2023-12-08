using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Models;
using SistemaFacturacion.Datos;
using Microsoft.AspNetCore.Authorization;

namespace SistemaFacturacion.Controllers
{
    public class RolController : Controller
    {
        RolDatos rd = new RolDatos();
        [Authorize(Roles = "Administrador")]
        public IActionResult Roles()
        {
            var list = rd.ListRoles();
            return View(list);
        }

        public IActionResult newRol()
        {

            return View();
        }
        [HttpPost]

        public IActionResult newRol(RolesModel oRol)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            if (!ModelState.IsValid)
            {
                return View();
            }

            var answer = rd.newRol(oRol);
            if (answer)
            {
                return RedirectToAction("Roles");
            }
            else
            {
                return View();
            }
        }

        public IActionResult editRol(int IdRol)
        {
            // Obtener el rol que estás editando
            var oRol = rd.getRoles(IdRol);

            // Obtener todos los roles
            List<RolesModel> roles = rd.ListRoles();

            // Asignar los roles a ViewBag.Roles
            ViewBag.Roles = roles;

            return View(oRol);
        }
        [HttpPost]

        public IActionResult editRol(RolesModel oRol)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            if (!ModelState.IsValid)
            {
                return View();
            }

            var answer = rd.editRol(oRol);
            if (answer)
            {
                return RedirectToAction("Roles");
            }
            else
            {
                return View();
            }
        }
        public IActionResult deleteRol(int IdRol)
        {
            var oRol = rd.getRoles(IdRol);
            // Devolver la vista para crear un nuevo cliente
            return View(oRol);
        }
        [HttpPost]

        public IActionResult deleteClient(RolesModel oRol)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            var answer = rd.deleteRol(oRol.IdRol);
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
