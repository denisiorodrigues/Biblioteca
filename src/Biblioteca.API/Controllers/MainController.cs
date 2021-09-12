using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.API.DTO;
using Biblioteca.Business.Interfaces;
using Biblioteca.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        //private readonly ILogger<MainController> _logger;

        // public MainController(ILogger<MainController> logger)
        // {
        //     _logger = logger;
        // }

        private readonly INotificador _notificador;

        public MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if(OperacaoValida())
            {
                return Ok(new 
                {
                    success = true,
                    data = result
                });
            }
           
           return BadRequest(new 
           {
               success = false,
               erros = _notificador.ObterNotificacoes().Select(x => x.Mensagem)
           });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if(!modelState.IsValid)
            {
                NotificarErroModelInvalida(modelState);
            }

            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(x => x.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(erroMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
    }
}