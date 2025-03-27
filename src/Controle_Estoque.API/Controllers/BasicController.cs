using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Domain.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace Controle_Estoque.API.Controllers
{
    [ApiController]
    public abstract class BasicController : Controller
    {
        private readonly INotificador _notificador;

        protected BasicController(INotificador notificador)
        {
            _notificador = notificador;
        }

        
        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null)
        {
            // Mensagens padrão para cada código de status
            string defaultMessage = statusCode switch
            {
                HttpStatusCode.Created => "Recurso criado com sucesso.", 
                HttpStatusCode.OK => "Operação realizada com sucesso.",
                HttpStatusCode.BadRequest => "Houve um erro na requisição.",
                HttpStatusCode.InternalServerError => "Erro interno do servidor.",
                _ => "Resposta processada com sucesso."
            };

            if (statusCode == HttpStatusCode.NoContent)
            {
                // Para status 204, não deve haver corpo na resposta
                return new ObjectResult(null)
                {
                    StatusCode = Convert.ToInt32(statusCode),
                };
            }

            if (OperacaoValida())
            {
                return new ObjectResult(new
                {
                    data = result,
                    message = defaultMessage // Mensagem padrão ou personalizada
                })
                {
                    StatusCode = Convert.ToInt32(statusCode),
                };
            }

            return BadRequest(new
            {
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }


        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

    }
}
