using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto3.Controllers
{
    public class ReportesController : Controller
    {
        private readonly HttpClient httpClient;

        public ReportesController()
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ReporteClientesAlfabeticamente(bool ascending = true)
        {
            var response = await httpClient.GetAsync("api/Clientes");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var clientes = JsonConvert.DeserializeObject<List<Cliente>>(json);

                // Realizar el ordenamiento alfabético
                if (ascending)
                {
                    clientes = clientes.OrderBy(c => c.Nombre.ToUpper()).ToList();
                }
                else
                {
                    clientes = clientes.OrderByDescending(c => c.Nombre.ToUpper()).ToList();
                }

                return View(clientes);
            }
            else
            {
                // Handle error response
                return View(new List<Cliente>());
            }
        }


        public async Task<IActionResult> ReporteVentasPorFecha(bool ascending = true)
        {
            var response = await httpClient.GetAsync("api/Ventas");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var ventas = JsonConvert.DeserializeObject<List<Venta>>(json);

                if (ascending)
                {
                    ventas = ventas.OrderBy(v => v.FechaHoraVenta).ToList();
                }
                else
                {
                    ventas = ventas.OrderByDescending(v => v.FechaHoraVenta).ToList();
                }

                return View(ventas);
            }
            else
            {
                // Handle error response
                return View(new List<Venta>());
            }
        }

        public async Task<IActionResult> ReporteReservasPorFecha(bool ascending = true)
        {
            var response = await httpClient.GetAsync("api/Reservas");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var reservas = JsonConvert.DeserializeObject<List<Reserva>>(json);

                if (ascending)
                {
                    reservas = reservas.OrderBy(r => r.FechaHoraReserva).ToList();
                }
                else
                {
                    reservas = reservas.OrderByDescending(r => r.FechaHoraReserva).ToList();
                }

                return View(reservas);
            }
            else
            {
                // Handle error response
                return View(new List<Reserva>());
            }
        }

        public async Task<IActionResult> ReportePlatosMasVendidos(bool ascending = false)
        {
            var responsePlatos = await httpClient.GetAsync("api/Platos");
            var responseVentas = await httpClient.GetAsync("api/Ventas");

            if (responsePlatos.IsSuccessStatusCode && responseVentas.IsSuccessStatusCode)
            {
                var jsonPlatos = await responsePlatos.Content.ReadAsStringAsync();
                var jsonVentas = await responseVentas.Content.ReadAsStringAsync();
                var platos = JsonConvert.DeserializeObject<List<Plato>>(jsonPlatos);
                var ventas = JsonConvert.DeserializeObject<List<Venta>>(jsonVentas);

                var reportePlatos = new List<ReportePlato>();

                foreach (var plato in platos)
                {
                    var cantidadVendida = ventas
                        .Where(v => v.NombrePlatoVendido == plato.Nombre)
                        .Sum(v => v.CantidadVendida);

                    var totalRecaudado = (decimal)cantidadVendida * plato.Precio;

                    reportePlatos.Add(new ReportePlato
                    {
                        NombrePlato = plato.Nombre,
                        CantidadVendida = cantidadVendida,
                        TotalRecaudado = totalRecaudado
                    });
                }

                if (ascending)
                {
                    reportePlatos = reportePlatos.OrderBy(rp => rp.CantidadVendida).ToList();
                }
                else
                {
                    reportePlatos = reportePlatos.OrderByDescending(rp => rp.CantidadVendida).ToList();
                }

                return View(reportePlatos);
            }
            else
            {
                // Handle error response
                return View(new List<ReportePlato>());
            }
        }


        public async Task<IActionResult> ReportePlatosVendidosPorMes(int mes, bool ascending = false)
        {
            var responsePlatos = await httpClient.GetAsync("api/Platos");
            var responseVentas = await httpClient.GetAsync("api/Ventas");

            if (responsePlatos.IsSuccessStatusCode && responseVentas.IsSuccessStatusCode)
            {
                var jsonPlatos = await responsePlatos.Content.ReadAsStringAsync();
                var jsonVentas = await responseVentas.Content.ReadAsStringAsync();
                var platos = JsonConvert.DeserializeObject<List<Plato>>(jsonPlatos);
                var ventas = JsonConvert.DeserializeObject<List<Venta>>(jsonVentas);

                var reportePlatos = new List<ReportePlato>();

                foreach (var plato in platos)
                {
                    var cantidadVendida = ventas
                        .Where(v => v.NombrePlatoVendido == plato.Nombre && v.FechaHoraVenta.Month == mes)
                        .Sum(v => v.CantidadVendida);

                    var totalRecaudado = (decimal)cantidadVendida * plato.Precio;

                    reportePlatos.Add(new ReportePlato
                    {
                        NombrePlato = plato.Nombre,
                        CantidadVendida = cantidadVendida,
                        TotalRecaudado = totalRecaudado
                    });
                }

                if (ascending)
                {
                    reportePlatos = reportePlatos.OrderBy(rp => rp.CantidadVendida).ToList();
                }
                else
                {
                    reportePlatos = reportePlatos.OrderByDescending(rp => rp.CantidadVendida).ToList();
                }

                return View("ReportePlatosVendidosPorMes", reportePlatos);
            }
            else
            {
                // Handle error response
                return View("ReportePlatosVendidosPorMes", new List<ReportePlato>());
            }
        }

        public async Task<IActionResult> ReporteVentasDelDia(DateTime fecha)
        {
            var responseVentas = await httpClient.GetAsync("api/Ventas");

            if (responseVentas.IsSuccessStatusCode)
            {
                var jsonVentas = await responseVentas.Content.ReadAsStringAsync();
                var ventas = JsonConvert.DeserializeObject<List<Venta>>(jsonVentas);

                var ventasDelDia = ventas
                    .Where(v => v.FechaHoraVenta.Date == fecha.Date)
                    .OrderBy(v => v.FechaHoraVenta)
                    .ToList();

                return View("ReporteVentasDelDia", ventasDelDia);
            }
            else
            {
                // Handle error response
                return View("ReporteVentasDelDia", new List<Venta>());
            }
        }


    }
}
