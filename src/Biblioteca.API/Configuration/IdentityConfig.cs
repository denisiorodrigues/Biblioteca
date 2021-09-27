using Biblioteca.API.Data;
using Biblioteca.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options => 
                    options.UseSqlite(configuration.GetConnectionString("SqliteConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddErrorDescriber<IdentityMensagensPortugues>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}