using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using todoApp_back.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoContext>(options => 
    options.UseSqlServer("Server=(localdb)\\localdb;Database=Todos;Trusted_Connection=True;MultipleActiveResultSets=true")
);

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
