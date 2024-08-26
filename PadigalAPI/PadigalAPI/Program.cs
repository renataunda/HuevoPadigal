using Microsoft.EntityFrameworkCore;
using PadigalAPI.Converters;
using PadigalAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PadigalAPI.Repositories;
using PadigalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión a la base de datos usando PadigalContext
builder.Services.AddDbContext<PadigalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura los servicios
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Agregar el conversor personalizado para ClientType
        options.SerializerSettings.Converters.Add(new ClientTypeConverter());
    });

builder.Services.AddControllers();
// Registrar los servicios y repositorios
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

var app = builder.Build();

// Configurar el enrutamiento de controladores
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Punto de inicio de la aplicación
app.MapGet("/", () => "API is running...");

app.Run();
