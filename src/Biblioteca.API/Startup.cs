using Biblioteca.API.Configuration;
using Biblioteca.Data.Context;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();

            services.AddDbContext<BibliotecaContext>( options => 
            {
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlite(Configuration.GetConnectionString("SqliteConnection"));
            });
            
            //Primeira configuração do Automapper
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
            
            //Desabilitando a validação automática dos campos para ter mais controle das validações
            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvc();

            services.ResolveDependencies();

            //Criando as permissões cors
            // services.AddCors(options => {
            //     options.AddPolicy("Development",builder => builder.AllowAnyOrigin()
            //     .AllowAnyMethod()
            //     .AllowAnyHeader()
            //     .AllowCredentials());
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            // app.UseCors("Development");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}