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
                    var errores = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return Json(new
                    {
                        success = false,
                        message = string.Join("|", errores)
                    });
                }

                model.Correos ??= new List<string>();

                using var transaction = await _context.Database.BeginTransactionAsync();

                var personaExiste = await _context.PersonaFisicas
                    .FirstOrDefaultAsync(x => x.Id == model.IdOriginal);

                if (personaExiste == null)
                {
                    // Nueva persona
                    _context.PersonaFisicas.Add(model);
                    await _context.SaveChangesAsync(); // EF asigna el Id

                    var personaId = model.Id;

                    foreach (var email in model.Correos)
                    {
                        var correo = new Correo
                        {
                            DescripcionCorreoPersona = email,
                            Estado = true
                        };

                        _context.Correos.Add(correo);
                        await _context.SaveChangesAsync();

                        _context.PersonaFisicaCorreos.Add(new PersonaFisicaCorreo
                        {
                            PersonaFisicaId = personaId,       // usar el Id ya generado
                            CorreoIdCorreo = correo.IdCorreo   // usar el Id del correo
                        });
                    }
                }
                else
                {
                    // Actualización
                    personaExiste.Cedula = model.Cedula;
                    personaExiste.Nombre = model.Nombre;
                    personaExiste.Apellido1 = model.Apellido1;
                    personaExiste.Apellido2 = model.Apellido2;
                    personaExiste.FechaNacimiento = model.FechaNacimiento;
                    personaExiste.Sexo = model.Sexo;
                    personaExiste.Nacionalidad = model.Nacionalidad;
                    personaExiste.Tipo = model.Tipo;
                    personaExiste.Estado = model.Estado;

                    // Eliminar relaciones y correos anteriores
                    var relaciones = await _context.PersonaFisicaCorreos
                        .Where(x => x.PersonaFisicaId == personaExiste.Id)
                        .ToListAsync();

                    var idsCorreos = relaciones.Select(x => x.CorreoIdCorreo).ToList();

                    _context.PersonaFisicaCorreos.RemoveRange(relaciones);

                    var correosViejos = await _context.Correos
                        .Where(x => idsCorreos.Contains(x.IdCorreo))
                        .ToListAsync();

                    _context.Correos.RemoveRange(correosViejos);

                    await _context.SaveChangesAsync();

                    // Insertar nuevos correos
                    foreach (var email in model.Correos)
                    {
                        var correo = new Correo
                        {
                            DescripcionCorreoPersona = email,
                            Estado = true
                        };

                        _context.Correos.Add(correo);
                        await _context.SaveChangesAsync();

                        _context.PersonaFisicaCorreos.Add(new PersonaFisicaCorreo
                        {
                            PersonaFisicaId = personaExiste.Id, // usar el Id real
                            CorreoIdCorreo = correo.IdCorreo
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Json(new
                {
                    success = true,
                    message = personaExiste == null
                        ? "Persona registrada correctamente."
                        : "Persona actualizada correctamente."
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



        [HttpGet]
        public async Task<IActionResult> Obtener(int id)
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
                        message = "Persona no encontrada."
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
                    message = ex.ToString()
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