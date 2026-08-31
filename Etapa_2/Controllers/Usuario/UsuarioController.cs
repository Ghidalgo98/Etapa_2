using Capa_Datos;
using Capa_Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Usuario.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BaseContext _context;

        public UsuarioController(BaseContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            try
            {
                var personas = await _context.PersonaFisicas
                    .Include(x => x.SexoNavigation)
                    .Include(x => x.NacionalidadNavigation)
                    .Include(x => x.TipoNavigation)
                    .ToListAsync();

                return View(personas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<PersonaFisica>());
            }
        }

        // GET: Usuario/Obtener/5
        [HttpGet]
        public async Task<IActionResult> Obtener(long id)
        {
            try
            {
                var persona = await _context.PersonaFisicas
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (persona == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Registro no encontrado."
                    });
                }

                return Json(new
                {
                    success = true,
                    data = persona
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        // POST: Usuario/Guardar
        [HttpPost]
        public async Task<IActionResult> Guardar(PersonaFisica model)
        {
            ModelState.Remove("SexoNavigation");
            
            ModelState.Remove("TipoNavigation");
            
            ModelState.Remove("NacionalidadNavigation");
            try
            {
                if (!ModelState.IsValid)
                {
                    var errores=ModelState.Values
                        .SelectMany(v=> v.Errors)
                        .Select(e=> e.ErrorMessage).ToList();
                    return Json(new
                    {
                        success = false,message=string.Join("|", errores)


                    });
                    
                }

                if (model.Id == 0)
                {
                    _context.PersonaFisicas.Add(model);
                }
                else
                {
                    var personaExiste = await _context.PersonaFisicas
                        .FirstOrDefaultAsync(x => x.Id == model.Id);

                    if (personaExiste == null)
                    {
                        return Json(new
                        {
                            success = false,
                            message = "El registro no existe."
                        });
                    }

                    personaExiste.Nombre = model.Nombre;
                    personaExiste.Apellido1 = model.Apellido1;
                    personaExiste.Apellido2 = model.Apellido2;
                    personaExiste.FechaNacimiento = model.FechaNacimiento;
                    personaExiste.Sexo = model.Sexo;
                    personaExiste.Nacionalidad = model.Nacionalidad;
                    personaExiste.Tipo = model.Tipo;
                    personaExiste.Estado = model.Estado;
                }

                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return Json(new
                    {
                        success = true,
                        message = model.Id == 0
                            ? "Persona registrada correctamente."
                            : "Persona actualizada correctamente."
                    });
                }

                return Json(new
                {
                    success = false,
                    message = "No se realizaron cambios."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        // GET: Usuario/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var persona = await _context.PersonaFisicas
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (persona == null)
                {
                    TempData["Error"] = "Registro no encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                _context.PersonaFisicas.Remove(persona);

                await _context.SaveChangesAsync();

                TempData["Success"] = "Registro eliminado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException?.Message ?? ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }
    }
}