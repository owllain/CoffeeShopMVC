using BaseDeDatos.Data;
using BaseDeDatos.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BaseDeDatos.Repository
{
    public class PlatosRepository
    {
        private readonly PlatosDBContext _context;

        public PlatosRepository(PlatosDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Plato> GetAllPlatos()
        {
            return _context.Platos.ToList();
        }

        public Plato GetPlatoByID(int id)
        {
            return _context.Platos.FirstOrDefault(p => p.ID == id);
        }

        public void InsertPlato(Plato plato)
        {
            _context.Platos.Add(plato);
            _context.SaveChanges();
        }

        public void UpdatePlato(Plato plato)
        {
            var existingPlato = _context.Platos.FirstOrDefault(p => p.ID == plato.ID);
            if (existingPlato != null)
            {
                // Actualizar las propiedades del plato existente con los valores proporcionados en el objeto "plato"
                existingPlato.Nombre = plato.Nombre;
                existingPlato.Descripcion = plato.Descripcion;
                existingPlato.ImagenData = plato.ImagenData;
                existingPlato.Precio = plato.Precio;
                existingPlato.Categoria = plato.Categoria;

                _context.SaveChanges();
            }
        }

        public void DeletePlato(int id)
        {
            var existingPlato = _context.Platos.FirstOrDefault(p => p.ID == id);
            if (existingPlato != null)
            {
                _context.Platos.Remove(existingPlato);
                _context.SaveChanges();
            }
        }
    }
}
