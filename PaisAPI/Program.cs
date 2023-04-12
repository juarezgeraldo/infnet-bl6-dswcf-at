using Domain;
using EstadoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using PaisAPI.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<MvcOptions>(c =>
    c.Conventions.Add(new SwaggerApplicationConvention()));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PaisAPI",
        Version = "v1"
    });
});

builder.Services.Configure<ConnectionStrings>(
    builder.Configuration.GetSection(ConnectionStrings.Name));

builder.Services.AddScoped<IBlobService, BlobService>();
builder.Services.AddScoped<IPaisService, PaisService>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<IEstatisticaService, EstatisticaService>();
builder.Services.AddTransient<BlobService, BlobService>();


var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
