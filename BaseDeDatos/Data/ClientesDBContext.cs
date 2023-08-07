using BaseDeDatos.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseDeDatos.Data
{
    public class ClientesDBContext : DbContext
    {
        public ClientesDBContext(DbContextOptions<ClientesDBContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
