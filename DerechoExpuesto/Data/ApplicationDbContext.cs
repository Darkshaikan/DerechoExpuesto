using DerechoExpuesto.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DerechoExpuesto.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Servicio> Servicios { get; set; }

        public DbSet<Consulta> Consultas { get; set; }

        public DbSet<ConfiguracionSitio> ConfiguracionesSitio { get; set; }
    }
}