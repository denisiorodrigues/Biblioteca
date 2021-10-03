using System.Text;
using Biblioteca.API.Data;
using Biblioteca.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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

            #region JWT 
            
            var appSettingSection = configuration.GetSection("JWTSettings");
            services.Configure<JWTSettings>(appSettingSection);

            var appSettingJWT = appSettingSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettingJWT.Secret);

            services.AddAuthentication(x => 
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // É usado para validar o token
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true; //O token deve ser guaradado no authentication properti
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // Validar quem está emitindo (Issuer) tem que ser o mesmo que foi emitido o token
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Transforma a chave para criptografada
                    ValidateIssuer = true, // Validar apenas o Issuer conforme o nome
                    ValidateAudience = true, // Onde seu token é válido, em qual audiencia?
                    ValidAudience = appSettingJWT.ValidoEm, // Informa a audiencia
                    ValidIssuer = appSettingJWT.Emissor // Informa o Issuer
                };
            });

            #endregion

            return services;
        }
    }
}