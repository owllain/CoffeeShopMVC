using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto3.Controllers
{
    public class VentasController : Controller
    {
        private readonly HttpClient httpClient;

        public VentasController()
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
            var response = await httpClient.GetAsync("api/Ventas");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var ventas = JsonConvert.DeserializeObject<List<Venta>>(json);

                return View(ventas);
            }
            else
            {
                // Handle error response
                return View(new List<Venta>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var venta = new Venta();

            // Asignar un valor aleatorio al número de orden de la venta
            Random random = new Random();
            venta.NumeroOrden = random.Next(1, 10000);

            var platos = GetPlatos().Result;
            ViewBag.Platos = platos;

            return View(venta);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Venta venta)
        {

            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // El modelo no es válido, volver a la vista con el modelo para mostrar los mensajes de error
                var platos = await GetPlatos();
                ViewBag.Platos = platos;
                return View(venta);
            }

            var json = JsonConvert.SerializeObject(venta);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/Ventas", content);

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
        public async Task<IActionResult> Edit(int numeroOrden)
        {
            var response = await httpClient.GetAsync($"api/Ventas/{numeroOrden}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var venta = JsonConvert.DeserializeObject<Venta>(json);

                var platos = GetPlatos().Result;
                ViewBag.Platos = platos;

                return View(venta);
            }
            else
            {
                // Handle error response
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Venta venta)
        {


            var json = JsonConvert.SerializeObject(venta);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"api/Ventas/{venta.NumeroOrden}", content);
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
        public async Task<IActionResult> Delete(int numeroOrden)
        {
            var response = await httpClient.GetAsync($"api/Ventas/{numeroOrden}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var venta = JsonConvert.DeserializeObject<Venta>(json);

                // Aquí puedes mostrar la vista de confirmación de eliminación con los datos de la venta
                return View(venta);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int numeroOrden)
        {
            var response = await httpClient.DeleteAsync($"api/Ventas/{numeroOrden}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private async Task<List<Plato>> GetPlatos()
        {
            var response = await httpClient.GetAsync("api/Platos");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var platos = JsonConvert.DeserializeObject<List<Plato>>(content);
                return platos;
            }
            else
            {
                return new List<Plato>();
            }
        }

      

    }
}
