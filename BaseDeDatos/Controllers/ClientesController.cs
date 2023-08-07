using BaseDeDatos.Entities;
using BaseDeDatos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesRepository _clientesRepository;

        public ClientesController(ClientesRepository clientesRepository)
        {
            _clientesRepository = clientesRepository;
        }

        [HttpGet]
        public IActionResult GetAllClientes()
        {
            var clientes = _clientesRepository.GetAllClientes();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult GetClienteByID(int id)
        {
            var cliente = _clientesRepository.GetClienteByID(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult InsertCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _clientesRepository.InsertCliente(cliente);
            return CreatedAtAction(nameof(GetClienteByID), new { id = cliente.ID }, cliente);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingCliente = _clientesRepository.GetClienteByID(id);
            if (existingCliente == null)
            {
                return NotFound();
            }

            cliente.ID = id; // Asegurarnos de que el ID del cliente sea el mismo que el del objeto en el cuerpo de la solicitud
            _clientesRepository.UpdateCliente(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            var existingCliente = _clientesRepository.GetClienteByID(id);
            if (existingCliente == null)
            {
                return NotFound();
            }

            _clientesRepository.DeleteCliente(id);
            return NoContent();
        }
    }
}
