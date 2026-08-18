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
            var modelo =
                await CrearHomeViewModel();


            return View(
                modelo
            );
        }


        // ============================================
        // ACCIDENTES DE TRABAJO
        // ============================================

        [HttpGet]
        [Route(
            "servicios/accidentes-de-trabajo"
        )]
        public async Task<IActionResult>
            AccidentesDeTrabajo()
        {
            var modelo =
                await CrearHomeViewModel();


            return View(
                "AccidentesDeTrabajo",
                modelo
            );
        }


        // ============================================
        // ENFERMEDADES PROFESIONALES
        // ============================================

        [HttpGet]
        [Route(
            "servicios/enfermedades-profesionales"
        )]
        public async Task<IActionResult>
            EnfermedadesProfesionales()
        {
            var modelo =
                await CrearHomeViewModel();


            return View(
                "EnfermedadesProfesionales",
                modelo
            );
        }


        // ============================================
        // DERECHO DE FAMILIA
        // ============================================

        [HttpGet]
        [Route(
            "servicios/derecho-de-familia"
        )]
        public async Task<IActionResult>
            DerechoDeFamilia()
        {
            var modelo =
                await CrearHomeViewModel();


            return View(
                "DerechoDeFamilia",
                modelo
            );
        }


        // ============================================
        // SUCESIONES
        // ============================================

        [HttpGet]
        [Route(
            "servicios/sucesiones"
        )]
        public async Task<IActionResult>
            Sucesiones()
        {
            var modelo =
                await CrearHomeViewModel();


            return View(
                "Sucesiones",
                modelo
            );
        }


        // ============================================
        // AMPAROS DE SALUD
        // ============================================

        [HttpGet]
        [Route(
            "servicios/amparos-de-salud"
        )]
        public async Task<IActionResult>
            AmparosDeSalud()
        {
            var modelo =
                await CrearHomeViewModel();


            return View(
                "AmparosDeSalud",
                modelo
            );
        }


        // ============================================
        // ASESORAMIENTO A EMPRESAS
        // ============================================

        [HttpGet]
        [Route(
            "servicios/asesoramiento-a-empresas"
        )]
        public async Task<IActionResult>
            AsesoramientoAEmpresas()
        {
            var modelo =
                await CrearHomeViewModel();


            return View(
                "AsesoramientoAEmpresas",
                modelo
            );
        }


        // ============================================
        // ASESORAMIENTO JURÍDICO
        // ============================================

        [HttpGet]
        [Route(
            "servicios/asesoramiento-juridico"
        )]
        public async Task<IActionResult>
            AsesoramientoJuridico()
        {
            var modelo =
                await CrearHomeViewModel();


            return View(
                "AsesoramientoJuridico",
                modelo
            );
        }


        // ============================================
        // ENVIAR CONSULTA DESDE HOME
        // ============================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnviarConsulta(
            HomeViewModel modelo)
        {
            await CompletarModelo(
                modelo
            );


            if (!ModelState.IsValid)
            {
                return View(
                    "Index",
                    modelo
                );
            }


            await GuardarConsulta(
                modelo
            );


            TempData["ConsultaEnviada"] =
                "Tu consulta fue enviada correctamente. Nos pondremos en contacto con vos a la brevedad.";


            return RedirectToAction(
                nameof(Index)
            );
        }


        // ============================================
        // ENVIAR CONSULTA
        // ACCIDENTES DE TRABAJO
        // ============================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(
            "servicios/accidentes-de-trabajo/consulta"
        )]
        public async Task<IActionResult>
            EnviarConsultaAccidentesDeTrabajo(
                HomeViewModel modelo)
        {
            await CompletarModelo(
                modelo
            );


            if (!ModelState.IsValid)
            {
                return View(
                    "AccidentesDeTrabajo",
                    modelo
                );
            }


            await GuardarConsulta(
                modelo
            );


            TempData["ConsultaEnviada"] =
                "Tu consulta fue enviada correctamente. Nos pondremos en contacto con vos a la brevedad.";


            return RedirectToAction(
                nameof(
                    AccidentesDeTrabajo
                )
            );
        }


        // ============================================
        // ENVIAR CONSULTA
        // ENFERMEDADES PROFESIONALES
        // ============================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(
            "servicios/enfermedades-profesionales/consulta"
        )]
        public async Task<IActionResult>
            EnviarConsultaEnfermedadesProfesionales(
                HomeViewModel modelo)
        {
            await CompletarModelo(
                modelo
            );


            if (!ModelState.IsValid)
            {
                return View(
                    "EnfermedadesProfesionales",
                    modelo
                );
            }


            await GuardarConsulta(
                modelo
            );


            TempData["ConsultaEnviada"] =
                "Tu consulta fue enviada correctamente. Nos pondremos en contacto con vos a la brevedad.";


            return RedirectToAction(
                nameof(
                    EnfermedadesProfesionales
                )
            );
        }


        // ============================================
        // ENVIAR CONSULTA
        // DERECHO DE FAMILIA
        // ============================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(
            "servicios/derecho-de-familia/consulta"
        )]
        public async Task<IActionResult>
            EnviarConsultaDerechoDeFamilia(
                HomeViewModel modelo)
        {
            await CompletarModelo(
                modelo
            );


            if (!ModelState.IsValid)
            {
                return View(
                    "DerechoDeFamilia",
                    modelo
                );
            }


            await GuardarConsulta(
                modelo
            );


            TempData["ConsultaEnviada"] =
                "Tu consulta fue enviada correctamente. Nos pondremos en contacto con vos a la brevedad.";


            return RedirectToAction(
                nameof(
                    DerechoDeFamilia
                )
            );
        }


        // ============================================
        // ENVIAR CONSULTA
        // SUCESIONES
        // ============================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(
            "servicios/sucesiones/consulta"
        )]
        public async Task<IActionResult>
            EnviarConsultaSucesiones(
                HomeViewModel modelo)
        {
            await CompletarModelo(
                modelo
            );


            if (!ModelState.IsValid)
            {
                return View(
                    "Sucesiones",
                    modelo
                );
            }


            await GuardarConsulta(
                modelo
            );


            TempData["ConsultaEnviada"] =
                "Tu consulta fue enviada correctamente. Nos pondremos en contacto con vos a la brevedad.";


            return RedirectToAction(
                nameof(
                    Sucesiones
                )
            );
        }


        // ============================================
        // ENVIAR CONSULTA
        // AMPAROS DE SALUD
        // ============================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(
            "servicios/amparos-de-salud/consulta"
        )]
        public async Task<IActionResult>
            EnviarConsultaAmparosDeSalud(
                HomeViewModel modelo)
        {
            await CompletarModelo(
                modelo
            );


            if (!ModelState.IsValid)
            {
                return View(
                    "AmparosDeSalud",
                    modelo
                );
            }


            await GuardarConsulta(
                modelo
            );


            TempData["ConsultaEnviada"] =
                "Tu consulta fue enviada correctamente. Nos pondremos en contacto con vos a la brevedad.";


            return RedirectToAction(
                nameof(
                    AmparosDeSalud
                )
            );
        }


        // ============================================
        // ENVIAR CONSULTA
        // ASESORAMIENTO A EMPRESAS
        // ============================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(
            "servicios/asesoramiento-a-empresas/consulta"
        )]
        public async Task<IActionResult>
            EnviarConsultaAsesoramientoAEmpresas(
                HomeViewModel modelo)
        {
            await CompletarModelo(
                modelo
            );


            if (!ModelState.IsValid)
            {
                return View(
                    "AsesoramientoAEmpresas",
                    modelo
                );
            }


            await GuardarConsulta(
                modelo
            );


            TempData["ConsultaEnviada"] =
                "Tu consulta fue enviada correctamente. Nos pondremos en contacto con vos a la brevedad.";


            return RedirectToAction(
                nameof(
                    AsesoramientoAEmpresas
                )
            );
        }


        // ============================================
        // ENVIAR CONSULTA
        // ASESORAMIENTO JURÍDICO
        // ============================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(
            "servicios/asesoramiento-juridico/consulta"
        )]
        public async Task<IActionResult>
            EnviarConsultaAsesoramientoJuridico(
                HomeViewModel modelo)
        {
            await CompletarModelo(
                modelo
            );


            if (!ModelState.IsValid)
            {
                return View(
                    "AsesoramientoJuridico",
                    modelo
                );
            }


            await GuardarConsulta(
                modelo
            );


            TempData["ConsultaEnviada"] =
                "Tu consulta fue enviada correctamente. Nos pondremos en contacto con vos a la brevedad.";


            return RedirectToAction(
                nameof(
                    AsesoramientoJuridico
                )
            );
        }


        // ============================================
        // CREAR MODELO PRINCIPAL
        // ============================================

        private async Task<HomeViewModel>
            CrearHomeViewModel()
        {
            var modelo =
                new HomeViewModel();


            await CompletarModelo(
                modelo
            );


            return modelo;
        }


        // ============================================
        // COMPLETAR MODELO
        // ============================================

        private async Task CompletarModelo(
            HomeViewModel modelo)
        {
            modelo.Servicios =
                await _context
                    .Servicios
                    .OrderBy(
                        servicio =>
                            servicio.Id
                    )
                    .ToListAsync();


            modelo.Configuracion =
                await ObtenerConfiguracion();
        }


        // ============================================
        // GUARDAR CONSULTA
        // ============================================

        private async Task GuardarConsulta(
            HomeViewModel modelo)
        {
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
                        DateTime.UtcNow,

                    Respondida =
                        false
                };


            _context
                .Consultas
                .Add(
                    consulta
                );


            await _context
                .SaveChangesAsync();
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


            _context
                .ConfiguracionesSitio
                .Add(
                    configuracion
                );


            await _context
                .SaveChangesAsync();


            return configuracion;
        }
    }
}