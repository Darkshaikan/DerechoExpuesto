using DerechoExpuesto.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DerechoExpuesto.Controllers
{
    [Authorize]
    [Route("Admin/Consultas")]
    public class ConsultasController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ConsultasController(
            ApplicationDbContext context)
        {
            _context = context;
        }


        // ============================================
        // LISTADO
        // ============================================

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var consultas =
                await _context.Consultas
                    .OrderBy(consulta => consulta.Respondida)
                    .ThenByDescending(consulta => consulta.Fecha)
                    .ToListAsync();


            return View(consultas);
        }


        // ============================================
        // VER CONSULTA
        // ============================================

        [HttpGet("Ver/{id}")]
        public async Task<IActionResult> Details(
            int id)
        {
            var consulta =
                await _context.Consultas
                    .FirstOrDefaultAsync(
                        consulta =>
                            consulta.Id == id
                    );


            if (consulta == null)
            {
                return NotFound();
            }


            return View(consulta);
        }


        // ============================================
        // CAMBIAR ESTADO
        // ============================================

        [HttpPost("CambiarEstado/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(
            int id,
            string? returnUrl = null)
        {
            var consulta =
                await _context.Consultas
                    .FindAsync(id);


            if (consulta == null)
            {
                return NotFound();
            }


            consulta.Respondida =
                !consulta.Respondida;


            await _context.SaveChangesAsync();


            if (
                !string.IsNullOrWhiteSpace(returnUrl)
                &&
                Url.IsLocalUrl(returnUrl)
            )
            {
                return Redirect(returnUrl);
            }


            return RedirectToAction(
                nameof(Index)
            );
        }


        // ============================================
        // ELIMINAR - CONFIRMACIÓN
        // ============================================

        [HttpGet("Eliminar/{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var consulta =
                await _context.Consultas
                    .FirstOrDefaultAsync(
                        consulta =>
                            consulta.Id == id
                    );


            if (consulta == null)
            {
                return NotFound();
            }


            return View(consulta);
        }


        // ============================================
        // ELIMINAR - POST
        // ============================================

        [HttpPost("Eliminar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(
            int id)
        {
            var consulta =
                await _context.Consultas
                    .FindAsync(id);


            if (consulta != null)
            {
                _context.Consultas.Remove(
                    consulta
                );


                await _context.SaveChangesAsync();
            }


            return RedirectToAction(
                nameof(Index)
            );
        }
    }
}