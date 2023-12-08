using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Models;
using SistemaFacturacion.Datos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace SistemaFacturacion.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioDatos _usuarioDatos;

        // Constructor de la clase UsuarioController
        public UsuarioController()
        {
            // Inicialización de la variable _usuarioDatos con una nueva instancia de la clase UsuarioDatos
            _usuarioDatos = new UsuarioDatos();
        }

        // Método para mostrar la vista de inicio de sesión
        public IActionResult Login()
        {
            // Devuelve la vista de inicio de sesión
            return View();
        }

        // Método para manejar la solicitud POST de inicio de sesión
        [HttpPost]
        public async Task<IActionResult> Login(UsuariosModel _usuario)
        {
            // Validación del usuario utilizando el correo y la contraseña proporcionados
            var usuario = _usuarioDatos.ValidarUsuario(_usuario.Correo, _usuario.Contraseña);

            // Si el usuario es válido
            if (usuario != null)
            {
                // Creación de una lista de reclamaciones para el usuario
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim("Correo", usuario.Correo)
            };
                // Adición del rol del usuario a las reclamaciones
                claims.Add(new Claim(ClaimTypes.Role, usuario.Roles.NombreRol));
                // Creación de una nueva identidad de reclamaciones utilizando las reclamaciones y el esquema de autenticación de cookies
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                // Inicio de sesión del usuario y establecimiento de la cookie de autenticación
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                // Redirección al usuario a la página de inicio
                return RedirectToAction("Index", "Home");
            }
            // Si el usuario no es válido
            else
            {
                // Devuelve la vista de inicio de sesión
                return View();
            }
        }
        // Método para cerrar la sesión del usuario
        public async Task<IActionResult> Logout()
        {
            // Cierre de la sesión del usuario y eliminación de la cookie de autenticación
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirección al usuario a la página de inicio de sesión
            return RedirectToAction("Login", "Usuario");
        }
        public IActionResult Usuarios(string searchString = null)
        {
            // Obtener una lista de usuarios
            var list = _usuarioDatos.ListUsers(searchString);
            // Devolver la vista con la lista de usuarios
            return View(list);
        }
        // Método para manejar la acción newUser
        public IActionResult newUser()
        {
            // Obtener los roles desde la base de datos
            List<RolesModel> roles = _usuarioDatos.GetRoles(); 

            // Asignar los roles a ViewBag.Roles
            ViewBag.Roles = roles;
            // Devolver la vista para crear un nuevo usuario
            return View();
        }
        [HttpPost]

        public IActionResult newUser(UsuariosModel oUsuario)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            if (!ModelState.IsValid)
            {
                return View();
            }

            var answer = _usuarioDatos.newUser(oUsuario);
            if (answer)
            {
                return RedirectToAction("Usuarios");
            }
            else
            {
                return View();
            }
        }

        public IActionResult editUser(int IdUser)
        {
            var oUsuario = _usuarioDatos.getUsers(IdUser);
            // Obtener los roles desde la base de datos
            List<RolesModel> roles = _usuarioDatos.GetRoles();

            // Asignar los roles a ViewBag.Roles
            ViewBag.Roles = roles;

            oUsuario.Roles.IdRol = _usuarioDatos.getUserRole(IdUser);
            // Devolver la vista para crear un nuevo usuario
            return View(oUsuario);
        }
        [HttpPost]

        public IActionResult editUser(UsuariosModel oUsuario)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            if (!ModelState.IsValid)
            {
                return View();
            }

            var answer = _usuarioDatos.editUser(oUsuario);
            if (answer)
            {
                return RedirectToAction("Usuarios");
            }
            else
            {
                return View();
            }
        }
        public IActionResult deleteUser(int IdUser)
        {
            var oUsuario = _usuarioDatos.getUsers(IdUser);
            // Devolver la vista para crear un nuevo cliente
            return View(oUsuario);
        }
        [HttpPost]

        public IActionResult deleteUser(UsuariosModel oUsuario)
        {
            // Metodo que recibe el objeto para guardarlo en la BD
            var answer = _usuarioDatos.deleteUser(oUsuario.IdUser);
            if (answer)
            {
                return RedirectToAction("Usuarios");
            }
            else
            {
                return View();
            }
        }
    }
}
