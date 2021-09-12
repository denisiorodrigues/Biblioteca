using Biblioteca.Business.Interfaces;
using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Interfaces.Services;
using Biblioteca.Business.Notifications;
using Biblioteca.Business.Services;
using Biblioteca.Data.Context;
using Biblioteca.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        /**/
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Context
            services.AddScoped<BibliotecaContext>();

            //Notificador
            services.AddScoped<INotificador,Notificador>();

            //Repository
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();

            //Services
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IAutorService, AutorService>();

            return services;
        }
    }
}