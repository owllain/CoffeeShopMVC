using BaseDeDatos.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseDeDatos.Data
{
    public class PlatosDBContext : DbContext
    {
        public PlatosDBContext(DbContextOptions<PlatosDBContext> options) : base(options)
        {
        }

        public DbSet<Plato> Platos { get; set; }
    }
}
