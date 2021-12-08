using Biblioteca.API.Extensions;
using Biblioteca.Business.Interfaces;
using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Interfaces.Services;
using Biblioteca.Business.Notifications;
using Biblioteca.Business.Services;
using Biblioteca.Data.Context;
using Biblioteca.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        /**/
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            // Context
            services.AddScoped<BibliotecaContext>();

            //Notificador
            services.AddScoped<INotificador,Notificador>();

            //Repository
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
            services.AddScoped<IEscritoRepository, EscritoRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            //Services
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<IEmprestimoService, EmprestimoService>();

            //Configuração de usuári para toda a aplicação
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}