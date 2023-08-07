using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto3.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto3.Controllers
{
	public class PlatosController : Controller
	{
		private readonly HttpClient httpClient;

		public PlatosController()
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
			var response = await httpClient.GetAsync("api/Platos");

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var platos = JsonConvert.DeserializeObject<List<Plato>>(json);

				return View(platos);
			}
			else
			{
				// Handle error response
				return View(new List<Plato>());
			}
		}

		[HttpGet]
		public IActionResult Create()
		{
			var plato = new Plato();

			// Asignar un valor aleatorio al ID del plato
			Random random = new Random();
			plato.ID = random.Next(1, 10000);

			return View(plato);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Plato plato, IFormFile imagenSubida)
		{
			if (imagenSubida != null && imagenSubida.Length > 0)
			{
				plato.ImagenData = await ConvertImageToByteArray(imagenSubida);
			}

			var json = JsonConvert.SerializeObject(plato);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync("api/Platos", content);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			else
			{
				// Handle error response
				ModelState.AddModelError("", "Error al crear el plato.");
			}

			return View(plato);
		}

		private async Task<byte[]> ConvertImageToByteArray(IFormFile imagenSubida)
		{
			using (var memoryStream = new MemoryStream())
			{
				await imagenSubida.CopyToAsync(memoryStream);
				return memoryStream.ToArray();
			}
		}


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await httpClient.GetAsync($"api/Platos/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var plato = JsonConvert.DeserializeObject<Plato>(json);

                return View(plato);
            }
            else
            {
                // Handle error response
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Plato plato, IFormFile imagenSubida)
        {
            if (imagenSubida != null && imagenSubida.Length > 0)
            {
                plato.ImagenData = await ConvertImageToByteArray(imagenSubida);
            }

            var json = JsonConvert.SerializeObject(plato);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"api/Platos/{plato.ID}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error response
                ModelState.AddModelError("", "Error al modificar el plato.");
            }

            return View(plato);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync($"api/Platos/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var plato = JsonConvert.DeserializeObject<Plato>(json);

                return View(plato);
            }
            else
            {
                // Handle error response
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await httpClient.DeleteAsync($"api/Platos/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error response
                ModelState.AddModelError("", "Error al eliminar el plato.");
                return View();
            }
        }


    }
}

