using BaseDeDatos.Entities;
using BaseDeDatos.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private readonly PlatosRepository _platosRepository;

        public PlatosController(PlatosRepository platosRepository)
        {
            _platosRepository = platosRepository;
        }

        [HttpGet]
        public IActionResult GetAllPlatos()
        {
            var platos = _platosRepository.GetAllPlatos();
            return Ok(platos);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlatoByID(int id)
        {
            var plato = _platosRepository.GetPlatoByID(id);
            if (plato == null)
            {
                return NotFound();
            }
            return Ok(plato);
        }

        [HttpPost]
        public IActionResult InsertPlato([FromBody] Plato plato)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }

            _platosRepository.InsertPlato(plato);
            return CreatedAtAction(nameof(GetPlatoByID), new { id = plato.ID }, plato);
        }


        [HttpPut("{id}")]
        public IActionResult UpdatePlato(int id, [FromBody] Plato plato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingPlato = _platosRepository.GetPlatoByID(id);
            if (existingPlato == null)
            {
                return NotFound();
            }

            plato.ID = id; // Asegurarnos de que el ID del plato sea el mismo que el del objeto en el cuerpo de la solicitud
            _platosRepository.UpdatePlato(plato);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlato(int id)
        {
            var existingPlato = _platosRepository.GetPlatoByID(id);
            if (existingPlato == null)
            {
                return NotFound();
            }

            _platosRepository.DeletePlato(id);
            return NoContent();
        }
    }
}
