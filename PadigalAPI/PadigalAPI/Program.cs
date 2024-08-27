using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PadigalAPI.Converters;
using PadigalAPI.Data;
using PadigalAPI.Mappers;
using PadigalAPI.Repositories;
using PadigalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar el logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Agregar AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

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

// Agregar servicios a la colección (antes de 'builder.Build()')
builder.Services.AddControllers();

// Agregar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PadigalAPI",
        Version = "v1",
        Description = "API documentation for Padigal",
        Contact = new OpenApiContact
        {
            Name = "Renata Aparicio",
            Email = "renataunda11@gmail.com",
        }
    });
});

// Registrar los servicios y repositorios
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

var app = builder.Build();

// Configurar el manejo global de excepciones
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500; // Código de estado por defecto para errores del servidor
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error != null)
        {
            var exception = exceptionHandlerPathFeature.Error;

            // Puedes crear un objeto de respuesta para el error
            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred. Please try again later.",
                Detailed = exception.Message
            };

            // Escribir el error en el log
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exception, "An unexpected error occurred");

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    });
});

// Configurar Swagger (después de 'app.UseRouting()')
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PadigalAPI v1");
        c.RoutePrefix = string.Empty; // Para que Swagger esté en la raíz (localhost:<puerto>/)
    });
}

// Configurar el enrutamiento de controladores
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Punto de inicio de la aplicación
app.MapGet("/", () => "API is running...");

app.Run();
