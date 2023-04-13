using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using AmigoAPI.Services;

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
                    Title = "AmigoAPI",
                    Version = "v1"
                });
            });
            services.AddSwaggerGen();

            services.AddScoped<IAmigoService, AmigoService>();
            services.AddScoped<IBlobService, BlobService>();
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
