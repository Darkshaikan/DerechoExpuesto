using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DerechoExpuesto.Data
{
    public class PostgreSqlApplicationDbContextFactory
        : IDesignTimeDbContextFactory<PostgreSqlApplicationDbContext>
    {
        public PostgreSqlApplicationDbContext CreateDbContext(
            string[] args)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<ApplicationDbContext>();


            // Esta conexión se utiliza SOLAMENTE
            // para que Entity Framework pueda
            // generar las migraciones PostgreSQL.
            //
            // No necesita existir realmente
            // para ejecutar Add-Migration.

            optionsBuilder.UseNpgsql(
                "Host=localhost;" +
                "Port=5432;" +
                "Database=derechoexpuesto;" +
                "Username=postgres;" +
                "Password=development"
            );


            return new PostgreSqlApplicationDbContext(
                optionsBuilder.Options
            );
        }
    }
}