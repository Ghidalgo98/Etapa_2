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
        public JsonResult Listar()
        {
            var personas = _context.PersonaFisicas.Select(p => new {
                id = p.Id,
                cedula = p.Cedula,
                nombreCompleto = p.Nombre + " " + p.Apellido1 + " " + p.Apellido2,
                fechaNacimiento = p.FechaNacimiento.ToString("dd/MM/yyyy"),
                sexo = p.Sexo,
                nacionalidad = p.Nacionalidad,
                estado = p.Estado ? "Activo" : "Inactivo",
                acciones = $"<button class='btn btn-warning btn-sm btnEditar' data-id='{p.Id}'>Editar</button>"
                           + $" <button class='btn btn-danger btn-sm btnEliminar' data-id='{p.Id}'>Eliminar</button>"
            }).ToList();

            return Json(new { data = personas });
        }



        //Obtener

        public async Task<IActionResult> Obtener(int id)
        {
            try
            {
                var persona = await _context.PersonaFisicas
                    .Include(p => p.PersonaFisicaCorreos)
                        .ThenInclude(pc => pc.CorreoIdCorreoNavigation)
                    //.Include(p => p.PersonaFisicaTelefonos)
                        //.ThenInclude(pt => pt.TelefonoIdTelefonoNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (persona == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Persona no encontrada."
                    });
                }

                // Proyección de correos
                var correos = persona.PersonaFisicaCorreos
                    .Select(pc => new
                    {
                        correoIdCorreo = pc.CorreoIdCorreo,
                        descripcionCorreoPersona = pc.CorreoIdCorreoNavigation.DescripcionCorreoPersona
                    })
                    .ToList();

                // Proyección de teléfonos
               /* var telefonos = persona.PersonaFisicaTelefonos
                    .Select(pt => new
                    {
                        telefonoIdTelefono = pt.TelefonoIdTelefono,
                        numeroTelefonoPersona = pt.TelefonoIdTelefonoNavigation.NumeroTelefonoPersona
                    })
                    .ToList();*/

                return Json(new
                {
                    success = true,
                    data = new
                    {
                        persona.Id,
                        persona.Cedula,
                        persona.Nombre,
                        persona.Apellido1,
                        persona.Apellido2,
                        persona.FechaNacimiento,
                        persona.Sexo,
                        persona.Nacionalidad,
                        persona.Tipo,
                        persona.Estado,
                        correos,
                       // telefonos
                    }
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



        // POST: Usuario/Delete/5

        [HttpPost]
        public async Task<JsonResult> Eliminar(long id)
        {
            try
            {
                var persona = await _context.PersonaFisicas
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (persona == null)
                {
                    return Json(new { success = false, message = "Registro no encontrado." });
                }

                _context.PersonaFisicas.Remove(persona);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Registro eliminado correctamente." });
            }
            catch (DbUpdateException) // 👈 específico para errores de FK
            {
                return Json(new
                {
                    success = false,
                    message = "No se puede eliminar la persona porque tiene correos, teléfonos u otros datos asociados. Elimine primero esos registros."
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Ocurrió un error inesperado al intentar eliminar."
                });
            }
        }




    }
}