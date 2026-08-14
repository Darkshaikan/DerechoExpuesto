using DerechoExpuesto.Data;
using DerechoExpuesto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DerechoExpuesto.Controllers
{
    [Authorize]
    [Route("Admin/Servicios")]
    public class ServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ServiciosController(
            ApplicationDbContext context)
        {
            _context = context;
        }


        // ============================================
        // LISTAR
        // ============================================

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var servicios =
                await _context.Servicios
                    .OrderBy(s => s.Id)
                    .ToListAsync();


            return View(servicios);
        }


        // ============================================
        // CREAR
        // ============================================

        [HttpGet("Crear")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("Crear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Servicio servicio)
        {
            if (!ModelState.IsValid)
            {
                return View(servicio);
            }


            _context.Servicios.Add(servicio);

            await _context.SaveChangesAsync();


            return RedirectToAction(
                nameof(Index)
            );
        }


        // ============================================
        // EDITAR
        // ============================================

        [HttpGet("Editar/{id}")]
        public async Task<IActionResult> Edit(
            int id)
        {
            var servicio =
                await _context.Servicios
                    .FindAsync(id);


            if (servicio == null)
            {
                return NotFound();
            }


            return View(servicio);
        }


        [HttpPost("Editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Servicio servicio)
        {
            if (id != servicio.Id)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                return View(servicio);
            }


            try
            {
                _context.Update(servicio);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool existe =
                    await _context.Servicios
                        .AnyAsync(
                            s => s.Id == servicio.Id
                        );


                if (!existe)
                {
                    return NotFound();
                }


                throw;
            }


            return RedirectToAction(
                nameof(Index)
            );
        }


        // ============================================
        // ELIMINAR
        // ============================================

        [HttpGet("Eliminar/{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var servicio =
                await _context.Servicios
                    .FirstOrDefaultAsync(
                        s => s.Id == id
                    );


            if (servicio == null)
            {
                return NotFound();
            }


            return View(servicio);
        }


        [HttpPost("Eliminar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(
            int id)
        {
            var servicio =
                await _context.Servicios
                    .FindAsync(id);


            if (servicio != null)
            {
                _context.Servicios.Remove(
                    servicio
                );


                await _context.SaveChangesAsync();
            }


            return RedirectToAction(
                nameof(Index)
            );
        }
    }
}