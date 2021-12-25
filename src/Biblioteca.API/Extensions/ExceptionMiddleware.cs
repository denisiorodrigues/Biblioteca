using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.API.Extensions 
{
    public class ExceptionMiddleware 
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //Passar para o próximo meddleware
                _next(httpContext);
            }
            catch(Exception e)
            {
                await HandleExceptionsAsync(httpContext, e);
            }
        }

        private static async Task HandleExceptionsAsync(HttpContext httpContext, Exception exception)
        {
            ///Salvar o log
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        }
    }
}