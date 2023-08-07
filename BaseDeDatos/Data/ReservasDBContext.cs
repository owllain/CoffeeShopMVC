using BaseDeDatos.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseDeDatos.Data
{
    public class ReservasDBContext : DbContext
    {
        public ReservasDBContext(DbContextOptions<ReservasDBContext> options) : base(options)
        {
        }

        public DbSet<Reserva> Reservas { get; set; }
    }
}
