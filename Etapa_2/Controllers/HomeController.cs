using Capa_Modelos; 
using Microsoft.AspNetCore.Mvc;
using Capa_Logica;

namespace TuProyecto.Controllers
{
    public class HomeController : Controller
    {
      
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
      


    }
}
