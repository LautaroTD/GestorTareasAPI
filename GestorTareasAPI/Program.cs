using Microsoft.EntityFrameworkCore;
using GestorTareasAPI.Models;
using GestorTareasAPI.Interfaces;
using GestorTareasAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GestorTareasDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITasksService, TasksService>(); //Trabajo evaluativo no pide interfaz, borrar si asi lo desea.

//politica de CORS para permitir solicitudes desde cualquier origen, método y encabezado
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll"); //politica CORS

app.UseAuthorization();

app.MapControllers();

app.Run();
