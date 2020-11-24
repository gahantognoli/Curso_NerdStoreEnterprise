using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NSE.Catalogo.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services
                .AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "NerdStore Enterprise Catálogo API",
                    Description = "Essa API faz parte do curso de ASP.NET Core Enterprise Applications",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact {  Name = "Gabriel Antognoli", Email = "gabriel_antognoli@hotmail.com" }
                }));
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
