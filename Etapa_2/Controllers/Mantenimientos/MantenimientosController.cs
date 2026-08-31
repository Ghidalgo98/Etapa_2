using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Etapa_1.Controllers.Mantenimientos
{
    public class MantenimientosController : Controller
    {
        // GET: MantenimientosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MantenimientosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MantenimientosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MantenimientosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MantenimientosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MantenimientosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MantenimientosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MantenimientosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
