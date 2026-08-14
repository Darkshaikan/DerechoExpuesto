using DerechoExpuesto.Data;
using DerechoExpuesto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DerechoExpuesto.Controllers
{
    [Authorize]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;


        public AdminController(
            ApplicationDbContext context)
        {
            _context = context;
        }


        // ============================================
        // DASHBOARD
        // ============================================

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var modelo =
                new AdminDashboardViewModel
                {
                    CantidadServicios =
                        await _context.Servicios.CountAsync(),

                    CantidadConsultas =
                        await _context.Consultas.CountAsync(),

                    ConsultasNuevas =
                        await _context.Consultas.CountAsync(
                            consulta =>
                                !consulta.Respondida
                        ),

                    ConsultasRespondidas =
                        await _context.Consultas.CountAsync(
                            consulta =>
                                consulta.Respondida
                        )
                };


            return View(modelo);
        }
    }
}