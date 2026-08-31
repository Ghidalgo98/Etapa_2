using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Capa_Datos;
using Capa_Logica;
using Capa_Modelos;

namespace Etapa_1.Controllers
{
    public class Login : Controller
    {
        private readonly Usuario_Login _usuariologinLogico;
        public Login(Usuario_Login usuarioLogin) {

            _usuariologinLogico = usuarioLogin;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel ModeloLogin)
        {
            var usuario = _usuariologinLogico.Login(ModeloLogin.Username,ModeloLogin.Password);

            if (usuario != null)
            {
                HttpContext.Session.SetString("Usuario", usuario.Usuario_Logueo);
                TempData["Login Exitoso"] = "Login Exitoso!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View(ModeloLogin);
            }


        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["LogoutExitoso"] = "¡Cierre de sesión exitoso!";
            return RedirectToAction("Index", "Login");
        }
    }
}
