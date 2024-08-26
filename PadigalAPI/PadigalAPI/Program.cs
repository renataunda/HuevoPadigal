using Microsoft.EntityFrameworkCore;
using PadigalAPI.Data;
using PadigalAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión a la base de datos usando PadigalContext
builder.Services.AddDbContext<PadigalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar los servicios de controlador
builder.Services.AddControllers();

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
