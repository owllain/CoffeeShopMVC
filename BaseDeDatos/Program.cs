using BaseDeDatos.Data;
using BaseDeDatos.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);



// Configurar HttpClient para ignorar la validación del certificado SSL
var httpClient = new HttpClient(new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
});
httpClient.BaseAddress = new Uri("https://localhost:5001/"); // Reemplaza con la URL correcta de tu API

builder.Services.AddSingleton(httpClient);
// Configuración de la conexión a la base de datos
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();

// Configurar DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PlatosDBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ClientesDBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<VentasDBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ReservasDBContext>(options =>
    options.UseSqlServer(connectionString));

// Registrar el servicio Repository
builder.Services.AddScoped<PlatosRepository>();
builder.Services.AddScoped<ClientesRepository>();
builder.Services.AddScoped<VentasRepository>();
builder.Services.AddScoped<ReservasRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

 

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

