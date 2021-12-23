using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Biblioteca.API.Configuration
{

    public  static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => 
            {
              //Convencional
              //c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biblioteca", Version = "V1"});
              //Personalizada
                c.OperationFilter<SwaggerDefaultValues>();

                /* versão 4 (antiga) do swagger */
                // var security = new Dictionary<string, IEnumerable<string>>
                // {
                //   {"Bearer", new string[]{} }
                // };
                
                /* versão 5 (antiga) do swagger */
                var security = new OpenApiSecurityRequirement()
                {
                  {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                  }
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                  Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                  Name = "Authorization",
                  In = ParameterLocation.Header,
                  Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(security);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            //app.UseMiddleware<SwaggerAuthorizedMiddleware>();

            /*
                Vai dar um FOREACh e navegar em todas as versões configuradas 
                e vai gerar uma página para cada versão de
            */

            app.UseSwagger();
            app.UseSwaggerUI(option => {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    option.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }
    }

  public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
  {
    readonly IApiVersionDescriptionProvider provider;
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;
    
    public void Configure(SwaggerGenOptions options)
    {
      foreach (var description in provider.ApiVersionDescriptions)
      {
        options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
      }
    }

    static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
      var url = new Uri("http://www.opensource.org/licenses/MIT");
      var info = new OpenApiInfo()
      {
        Title = "API - dentech",
        Version = description.ApiVersion.ToString(), 
        Description = "Esta API faz parte do curso de REST com ASP.NET Core WebAPI",
        Contact = new OpenApiContact(){Name = "Eduardo Pires", Email= "contato@desenvolvedor.io"},
        TermsOfService= url,
        License = new OpenApiLicense { Name = "MIT", Url = url }
      };

      if (description.IsDeprecated)
      {
        info.Description += " esta versão está obsoleta";
      }

      return info;
    }
  }

  public class SwaggerDefaultValues : IOperationFilter
  {
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
      var apiDescription = context.ApiDescription;
      operation.Deprecated = apiDescription .IsDeprecated();

        if(operation.Parameters == null)
        {
            return;
        }

        foreach(var parameter in operation.Parameters.OfType<OpenApiParameter>())
        {
            var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
            if(parameter.Description == null)
            {
                parameter.Description = description.ModelMetadata?.Description;
            }

            // if(parameter. == null)
            // {
            //     parameter.Description = description.ModelMetadata?.Description;
            // }

            parameter.Required |= description.IsRequired;
        }
    }
  }

  // Adicionando um middle para mostrar o swagger se estiver logado
  public class SwaggerAuthorizedMiddleware
  {
    private readonly RequestDelegate _next;

    public SwaggerAuthorizedMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
      if(context.Request.Path.StartsWithSegments("/swagger") 
        && !context.User.Identity.IsAuthenticated)
      {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return;
      }

      await _next.Invoke(context);
    }
  }
}