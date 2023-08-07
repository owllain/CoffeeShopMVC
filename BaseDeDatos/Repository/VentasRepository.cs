using BaseDeDatos.Data;
using BaseDeDatos.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BaseDeDatos.Repository
{
    public class VentasRepository
    {
        private readonly VentasDBContext _context;

        public VentasRepository(VentasDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Venta> GetAllVentas()
        {
            return _context.Ventas.ToList();
        }

        public Venta GetVentaByNumeroOrden(int numeroOrden)
        {
            return _context.Ventas.FirstOrDefault(v => v.NumeroOrden == numeroOrden);
        }

        public void InsertVenta(Venta venta)
        {
            _context.Ventas.Add(venta);
            _context.SaveChanges();
        }

        public void UpdateVenta(Venta venta)
        {
            var existingVenta = _context.Ventas.FirstOrDefault(v => v.NumeroOrden == venta.NumeroOrden);
            if (existingVenta != null)
            {
                // Actualizar las propiedades de la venta existente con los valores proporcionados en el objeto "venta"
                existingVenta.FechaHoraVenta = venta.FechaHoraVenta;
                existingVenta.NombrePlatoVendido = venta.NombrePlatoVendido;
                existingVenta.CantidadVendida = venta.CantidadVendida;

                _context.SaveChanges();
            }
        }

        public void DeleteVenta(int numeroOrden)
        {
            var existingVenta = _context.Ventas.FirstOrDefault(v => v.NumeroOrden == numeroOrden);
            if (existingVenta != null)
            {
                _context.Ventas.Remove(existingVenta);
                _context.SaveChanges();
            }
        }
    }
}
