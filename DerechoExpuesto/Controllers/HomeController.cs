using DerechoExpuesto.Data;
using DerechoExpuesto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DerechoExpuesto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController(
            ApplicationDbContext context)
        {
            _context = context;
        }


        // ============================================
        // PÁGINA PRINCIPAL
        // ============================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var configuracion =
                await ObtenerConfiguracion();


            var modelo =
                new HomeViewModel
                {
                    Servicios =
                        await _context.Servicios
                            .OrderBy(
                                servicio =>
                                    servicio.Id
                            )
                            .ToListAsync(),

                    Configuracion =
                        configuracion
                };


            return View(modelo);
        }


        // ============================================
        // ENVIAR CONSULTA
        // ============================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnviarConsulta(
            HomeViewModel modelo)
        {
            modelo.Servicios =
                await _context.Servicios
                    .OrderBy(
                        servicio =>
                            servicio.Id
                    )
                    .ToListAsync();


            modelo.Configuracion =
                await ObtenerConfiguracion();


            if (!ModelState.IsValid)
            {
                return View(
                    "Index",
                    modelo
                );
            }


            var consulta =
                new Consulta
                {
                    Nombre =
                        modelo.Contacto.Nombre,

                    Email =
                        modelo.Contacto.Email,

                    Telefono =
                        modelo.Contacto.Telefono,

                    Mensaje =
                        modelo.Contacto.Mensaje,

                    // IMPORTANTE:
                    // PostgreSQL + Npgsql requiere UTC
                    // para timestamp with time zone.

                    Fecha =
                        DateTime.UtcNow,

                    Respondida =
                        false
                };


            _context.Consultas.Add(
                consulta
            );


            await _context.SaveChangesAsync();


            TempData["ConsultaEnviada"] =
                "Tu consulta fue enviada correctamente. Nos pondremos en contacto con vos a la brevedad.";


            return RedirectToAction(
                nameof(Index)
            );
        }


        // ============================================
        // OBTENER CONFIGURACIÓN
        // ============================================

        private async Task<ConfiguracionSitio>
            ObtenerConfiguracion()
        {
            var configuracion =
                await _context
                    .ConfiguracionesSitio
                    .FirstOrDefaultAsync();


            if (configuracion != null)
            {
                return configuracion;
            }


            configuracion =
                new ConfiguracionSitio
                {
                    Telefono =
                        "+54 9 11 3067-0533",

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


            return configuracion;
        }
    }
}