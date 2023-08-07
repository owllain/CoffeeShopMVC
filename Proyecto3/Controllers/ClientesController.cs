using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto3.Models.Entities;

namespace Proyecto3.Controllers
{
    public class ClientesController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClientesController()
        {
            // Configurar el HttpClient para ignorar la validación del certificado SSL
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri("https://localhost:5001/") // Cambiar la URL base según la dirección de tu API
            };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Clientes");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var clientes = JsonConvert.DeserializeObject<List<Cliente>>(content);

                // Aquí puedes procesar los clientes obtenidos y mostrarlos en la vista
                return View(clientes);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // El modelo no es válido, volver a la vista con el modelo para mostrar los mensajes de error
                return View(cliente);
            }

            // Generar un número aleatorio del 1 al 9999 para el ID
            Random random = new Random();
            cliente.ID = random.Next(1, 10000);

            var json = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Clientes", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }



        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/Clientes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Cliente>(content);

                // Aquí puedes mostrar el cliente en la vista para la edición
                return View(cliente);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            var json = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Clientes/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/Clientes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Cliente>(content);

                // Aquí puedes mostrar la vista de confirmación de eliminación con los datos del cliente
                return View(cliente);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Clientes/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}

