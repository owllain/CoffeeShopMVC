using BaseDeDatos.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseDeDatos.Data
{
    public class VentasDBContext : DbContext
    {
        public VentasDBContext(DbContextOptions<VentasDBContext> options) : base(options)
        {
        }

        public DbSet<Venta> Ventas { get; set; }
    }
}
