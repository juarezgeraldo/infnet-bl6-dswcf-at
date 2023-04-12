using EstadoAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using PaisAPI.Services;

namespace PaisApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.ConfigureSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PaisAPI",
                    Version = "v1"
                });
            });
            services.AddSwaggerGen();

            services.AddScoped<IPaisService, PaisService>();
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IEstatisticaService, EstatisticaService>();
            services.AddTransient<BlobService, BlobService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("policy");

            app.UseHttpsRedirection();

            app.UseAuthorization();
        }
    }
}
