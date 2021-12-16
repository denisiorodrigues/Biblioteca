using Biblioteca.API.Configuration;
using Biblioteca.Data.Context;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Biblioteca.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();

            services.AddDbContext<BibliotecaContext>( options => 
            {
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlite(Configuration.GetConnectionString("SqliteConnection"));
            });

            services.AddIdentityConfiguration(Configuration);
            
            services.WebApiConfig();

            //Primeira configuração do Automapper
            services.AddAutoMapper(typeof(Startup));
            
            services.AddSwaggerConfig();

            services.ResolveDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }
            else 
            {
               app.UseCors("Production");
               app.UseHsts();
            }
           
            app.UseMvcConfiguration();

            app.UseSwaggerConfig(provider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}