using BaseDeDatos.Data;
using BaseDeDatos.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BaseDeDatos.Repository
{
    public class ReservasRepository
    {
        private readonly ReservasDBContext _context;

        public ReservasRepository(ReservasDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Reserva> GetAllReservas()
        {
            return _context.Reservas.ToList();
        }

        public Reserva GetReservaByNumeroReserva(int numeroReserva)
        {
            return _context.Reservas.FirstOrDefault(r => r.NumeroReserva == numeroReserva);
        }

        public void InsertReserva(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
        }

        public void UpdateReserva(Reserva reserva)
        {
            var existingReserva = _context.Reservas.FirstOrDefault(r => r.NumeroReserva == reserva.NumeroReserva);
            if (existingReserva != null)
            {
                // Actualizar las propiedades de la reserva existente con los valores proporcionados en el objeto "reserva"
                existingReserva.FechaHoraReserva = reserva.FechaHoraReserva;
                existingReserva.NombreCliente = reserva.NombreCliente;
                existingReserva.NumPersonas = reserva.NumPersonas;

                _context.SaveChanges();
            }
        }

        public void DeleteReserva(int numeroReserva)
        {
            var existingReserva = _context.Reservas.FirstOrDefault(r => r.NumeroReserva == numeroReserva);
            if (existingReserva != null)
            {
                _context.Reservas.Remove(existingReserva);
                _context.SaveChanges();
            }
        }
    }
}
