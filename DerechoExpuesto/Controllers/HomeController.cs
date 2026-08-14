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
            /*
             * Servicios y Configuracion no vienen
             * desde el formulario.
             *
             * Los volvemos a cargar desde
             * la base de datos para poder
             * reconstruir correctamente la página
             * si existe algún error de validación.
             */

            modelo.Servicios =
                await _context.Servicios
                    .OrderBy(
                        servicio =>
                            servicio.Id
                    )
                    .ToListAsync();


            modelo.Configuracion =
                await ObtenerConfiguracion();


            // ========================================
            // VALIDACIÓN DEL FORMULARIO
            // ========================================

            if (!ModelState.IsValid)
            {
                return View(
                    "Index",
                    modelo
                );
            }


            // ========================================
            // CREAR CONSULTA
            // ========================================

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

                    Fecha =
                        DateTime.Now,

                    Respondida =
                        false
                };


            // ========================================
            // GUARDAR EN SQLITE
            // ========================================

            _context.Consultas.Add(
                consulta
            );


            await _context.SaveChangesAsync();


            // ========================================
            // MENSAJE DE CONFIRMACIÓN
            // ========================================

            TempData["ConsultaEnviada"] =
                "Tu consulta fue enviada correctamente. Nos pondremos en contacto con vos a la brevedad.";


            /*
             * Redirigimos después de guardar.
             *
             * Esto también evita que al refrescar
             * el navegador se vuelva a enviar
             * la misma consulta.
             */

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


            // ========================================
            // CONFIGURACIÓN POR DEFECTO
            // ========================================

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