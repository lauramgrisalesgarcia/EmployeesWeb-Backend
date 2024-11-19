using Microsoft.EntityFrameworkCore;
using EmployeesWeb.Models;

var MyAllowSpecificOrigins = "_MyAllowSubdomainPolicy";
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<Connection>(opt => opt.UseInMemoryDatabase("Employees"));

// Description: Add CORS policity to allow all the origins, methods and headers. It is only for development
// Author: Laura Grisales
// Date: 18/11/2024
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("*");
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(MyAllowSpecificOrigins);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
