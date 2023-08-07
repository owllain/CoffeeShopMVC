using BaseDeDatos.Entities;
using BaseDeDatos.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ReservasRepository _reservasRepository;

        public ReservasController(ReservasRepository reservasRepository)
        {
            _reservasRepository = reservasRepository;
        }

        [HttpGet]
        public IActionResult GetAllReservas()
        {
            var reservas = _reservasRepository.GetAllReservas();
            return Ok(reservas);
        }

        [HttpGet("{numeroReserva}")]
        public IActionResult GetReservaByNumeroReserva(int numeroReserva)
        {
            var reserva = _reservasRepository.GetReservaByNumeroReserva(numeroReserva);
            if (reserva == null)
            {
                return NotFound();
            }
            return Ok(reserva);
        }

        [HttpPost]
        public IActionResult InsertReserva([FromBody] Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _reservasRepository.InsertReserva(reserva);
            return CreatedAtAction(nameof(GetReservaByNumeroReserva), new { numeroReserva = reserva.NumeroReserva }, reserva);
        }

        [HttpPut("{numeroReserva}")]
        public IActionResult UpdateReserva(int numeroReserva, [FromBody] Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingReserva = _reservasRepository.GetReservaByNumeroReserva(numeroReserva);
            if (existingReserva == null)
            {
                return NotFound();
            }

            reserva.NumeroReserva = numeroReserva; // Asegurarnos de que el número de reserva sea el mismo que el del objeto en el cuerpo de la solicitud
            _reservasRepository.UpdateReserva(reserva);
            return NoContent();
        }

        [HttpDelete("{numeroReserva}")]
        public IActionResult DeleteReserva(int numeroReserva)
        {
            var existingReserva = _reservasRepository.GetReservaByNumeroReserva(numeroReserva);
            if (existingReserva == null)
            {
                return NotFound();
            }

            _reservasRepository.DeleteReserva(numeroReserva);
            return NoContent();
        }
    }
}
