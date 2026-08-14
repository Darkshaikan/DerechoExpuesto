using DerechoExpuesto.Data;
using DerechoExpuesto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DerechoExpuesto.Controllers
{
    [Authorize]
    [Route("Admin/Configuracion")]
    public class ConfiguracionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConfiguracionController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var configuracion =
                await _context.ConfiguracionesSitio
                    .FirstOrDefaultAsync();

            if (configuracion == null)
            {
                configuracion = new ConfiguracionSitio
                {
                    Telefono = "+54 9 11 3067-0533",

                    Email =
                        "derechoexpuesto@gmail.com",

                    Horario =
                        "Lun a Vie de 9 a 17 hs",

                    WhatsApp =
                        "5491130670533",

                    Instagram =
                        "https://www.instagram.com/derechoexpuesto"
                };

                _context.ConfiguracionesSitio.Add(
                    configuracion
                );

                await _context.SaveChangesAsync();
            }

            return View(configuracion);
        }

        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(
            ConfiguracionSitio configuracion)
        {
            if (!ModelState.IsValid)
            {
                return View(configuracion);
            }

            var configuracionActual =
                await _context.ConfiguracionesSitio
                    .FirstOrDefaultAsync();

            if (configuracionActual == null)
            {
                _context.ConfiguracionesSitio.Add(
                    configuracion
                );
            }
            else
            {
                configuracionActual.Telefono =
                    configuracion.Telefono;

                configuracionActual.Email =
                    configuracion.Email;

                configuracionActual.Horario =
                    configuracion.Horario;

                configuracionActual.WhatsApp =
                    configuracion.WhatsApp;

                configuracionActual.Instagram =
                    configuracion.Instagram;
            }

            await _context.SaveChangesAsync();

            TempData["ConfiguracionGuardada"] =
                "La configuración fue actualizada correctamente.";

            return RedirectToAction(
                nameof(Index)
            );
        }
    }
}