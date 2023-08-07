using BaseDeDatos.Data;
using BaseDeDatos.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BaseDeDatos.Repository
{
    public class ClientesRepository
    {
        private readonly ClientesDBContext _context;

        public ClientesRepository(ClientesDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Cliente> GetAllClientes()
        {
            return _context.Clientes.ToList();
        }

        public Cliente GetClienteByID(int id)
        {
            return _context.Clientes.FirstOrDefault(c => c.ID == id);
        }

        public void InsertCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public void UpdateCliente(Cliente cliente)
        {
            var existingCliente = _context.Clientes.FirstOrDefault(c => c.ID == cliente.ID);
            if (existingCliente != null)
            {
                // Actualizar las propiedades del cliente existente con los valores proporcionados en el objeto "cliente"
                existingCliente.Nombre = cliente.Nombre;
                existingCliente.Apellido = cliente.Apellido;
                existingCliente.Email = cliente.Email;
                existingCliente.Privilegio = cliente.Privilegio;

                _context.SaveChanges();
            }
        }

        public void DeleteCliente(int id)
        {
            var existingCliente = _context.Clientes.FirstOrDefault(c => c.ID == id);
            if (existingCliente != null)
            {
                _context.Clientes.Remove(existingCliente);
                _context.SaveChanges();
            }
        }
    }
}
