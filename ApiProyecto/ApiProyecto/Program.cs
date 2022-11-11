// 1. A�ADIR LOS USING
using Microsoft.EntityFrameworkCore;
using ApiProyecto.DataAccess;

var builder = WebApplication.CreateBuilder(args);


// 2. Conexion con la BD
const string CONEXIONBD = "ProyectoMFE";
var conexionString = builder.Configuration.GetConnectionString(CONEXIONBD);

// 3. A�adir Context
builder.Services.AddDbContext<ProyectoMFEContext>(options =>options.UseSqlServer(conexionString));



// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
