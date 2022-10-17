using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//InMemory
//builder.Services.AddDbContext<FlightContext>(opt =>
//    opt.UseInMemoryDatabase("FlightClientList"));



// SQL Server
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connection = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
connection.Password = builder.Configuration["DbFlightDevPassword"];
var connectionString = connection.ConnectionString;

builder.Services.AddDbContext<FlightContext>(options =>
    options.UseSqlServer(connectionString));


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
