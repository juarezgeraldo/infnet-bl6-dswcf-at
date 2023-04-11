using Domain;
using EstadoAPI.Services;
using PaisAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ConnectionStrings>(
    builder.Configuration.GetSection(ConnectionStrings.Name));

builder.Services.AddScoped<IBlobService, BlobService>();
builder.Services.AddScoped<IPaisService, PaisService>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddTransient<BlobService, BlobService>();

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
