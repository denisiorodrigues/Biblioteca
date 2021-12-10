using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace Biblioteca.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            services.AddApiVersioning(options => {
              options.AssumeDefaultVersionWhenUnspecified = true;
              options.DefaultApiVersion = new ApiVersion(1,0);
              options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(option => {
              option.GroupNameFormat = "'v'VVV";
              option.SubstituteApiVersionInUrl = true;
            });

            services.AddControllers();
            
            //Desabilitando a validação automática dos campos para ter mais controle das validações
            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Criando as permissões cors
            // services.AddCors(options => {
            //     options.AddPolicy("Development",builder => builder.AllowAnyOrigin()
            //     .AllowAnyMethod()
            //     .AllowAnyHeader()
            //     .AllowCredentials());
            // });

            services.AddCors(options => {
                options.AddPolicy("Production",builder => 
                builder.WithMethods("GET")
                .WithOrigins("http://desevolvedor.io")
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
                .AllowAnyHeader());
            });

            return services;
        }

        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            
            //app.UseCors("Development");
            //app.UseMvc();

            return app;
        }
    }
}