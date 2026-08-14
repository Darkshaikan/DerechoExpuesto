using Microsoft.EntityFrameworkCore;

namespace DerechoExpuesto.Data
{
    public class PostgreSqlApplicationDbContext
        : ApplicationDbContext
    {
        public PostgreSqlApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}