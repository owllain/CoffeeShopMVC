using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Configurar HttpClient para ignorar la validación del certificado SSL
var httpClient = new HttpClient(new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
});
httpClient.BaseAddress = new Uri("https://localhost:7067/"); // Reemplaza con la URL correcta de tu API

// Add services to the container.
builder.Services.AddSingleton(httpClient);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
