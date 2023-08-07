using BaseDeDatos.Entities;
using BaseDeDatos.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly VentasRepository _ventasRepository;

        public VentasController(VentasRepository ventasRepository)
        {
            _ventasRepository = ventasRepository;
        }

        [HttpGet]
        public IActionResult GetAllVentas()
        {
            var ventas = _ventasRepository.GetAllVentas();
            return Ok(ventas);
        }

        [HttpGet("{numeroOrden}")]
        public IActionResult GetVentaByNumeroOrden(int numeroOrden)
        {
            var venta = _ventasRepository.GetVentaByNumeroOrden(numeroOrden);
            if (venta == null)
            {
                return NotFound();
            }
            return Ok(venta);
        }

        [HttpPost]
        public IActionResult InsertVenta([FromBody] Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _ventasRepository.InsertVenta(venta);
            return CreatedAtAction(nameof(GetVentaByNumeroOrden), new { numeroOrden = venta.NumeroOrden }, venta);
        }

        [HttpPut("{numeroOrden}")]
        public IActionResult UpdateVenta(int numeroOrden, [FromBody] Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingVenta = _ventasRepository.GetVentaByNumeroOrden(numeroOrden);
            if (existingVenta == null)
            {
                return NotFound();
            }

            venta.NumeroOrden = numeroOrden; // Asegurarnos de que el número de orden de la venta sea el mismo que el del objeto en el cuerpo de la solicitud
            _ventasRepository.UpdateVenta(venta);
            return NoContent();
        }

        [HttpDelete("{numeroOrden}")]
        public IActionResult DeleteVenta(int numeroOrden)
        {
            var existingVenta = _ventasRepository.GetVentaByNumeroOrden(numeroOrden);
            if (existingVenta == null)
            {
                return NotFound();
            }

            _ventasRepository.DeleteVenta(numeroOrden);
            return NoContent();
        }
    }
}
