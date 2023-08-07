using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Proyecto3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto3.Controllers
{
    public class ReservasController : Controller
    {
        private readonly HttpClient httpClient;

        public ReservasController()
        {
            // Configurar el HttpClient para ignorar la validación del certificado SSL
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri("https://localhost:5001/") // Cambiar la URL base según la dirección de tu API
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await httpClient.GetAsync("api/Reservas");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var reservas = JsonConvert.DeserializeObject<List<Reserva>>(json);

                return View(reservas);
            }
            else
            {
                // Handle error response
                return View(new List<Reserva>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var reserva = new Reserva();

            // Asignar un valor aleatorio al número de reserva
            Random random = new Random();
            reserva.NumeroReserva = random.Next(1, 10000);

            var clientes = GetClientes().Result.Select(c => $"{c.Nombre} {c.Apellido}");
            ViewBag.Clientes = clientes;

            return View(reserva);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // El modelo no es válido, volver a la vista con el modelo para mostrar los mensajes de error
                var clientes = await GetClientes();
                ViewBag.Clientes = clientes;
                return View(reserva);
            }

            var json = JsonConvert.SerializeObject(reserva);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/Reservas", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error response
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int numeroReserva)
        {
            var response = await httpClient.GetAsync($"api/Reservas/{numeroReserva}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var reserva = JsonConvert.DeserializeObject<Reserva>(json);

                var clientes = GetClientes().Result.Select(c => $"{c.Nombre} {c.Apellido}");
                ViewBag.Clientes = clientes;

                return View(reserva);
            }
            else
            {
                // Handle error response
                return NotFound();
            }
        }



        [HttpPost]
        public async Task<IActionResult> Edit(Reserva reserva)
        {


            var json = JsonConvert.SerializeObject(reserva);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"api/Reservas/{reserva.NumeroReserva}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error response
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int numeroReserva)
        {
            var response = await httpClient.GetAsync($"api/Reservas/{numeroReserva}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var reserva = JsonConvert.DeserializeObject<Reserva>(json);

                // Aquí puedes mostrar la vista de confirmación de eliminación con los datos de la reserva
                return View(reserva);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int numeroReserva)
        {
            var response = await httpClient.DeleteAsync($"api/Reservas/{numeroReserva}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private async Task<List<Cliente>> GetClientes()
        {
            var response = await httpClient.GetAsync("api/Clientes");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var clientes = JsonConvert.DeserializeObject<List<Cliente>>(content);
                return clientes;
            }
            else
            {
                return new List<Cliente>();
            }
        }

    }
}
