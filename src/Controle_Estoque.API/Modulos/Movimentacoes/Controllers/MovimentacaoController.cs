using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Movimentacoes.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Entidades.Validacoes.Documento.Padronizar.Texto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_Estoque.API.Modulos.Movimentacoes.Controllers
{
    [ApiController]
    [Route("api/movimentacoes")]
    public class MovimentacaoController : BasicController
    {
        private readonly IMovimentacaoRepositorio _movimentacaoRepositorio;
        private readonly IMovimentacaoServicos _movimentacaoServicos;
        private readonly IMapper _mapper;

        public MovimentacaoController(IMovimentacaoRepositorio movimentacaoRepositorio, 
            IMovimentacaoServicos movimentacaoServicos, 
            IMapper mapper, 
            INotificador notificador) : base(notificador)
        {
            _movimentacaoRepositorio = movimentacaoRepositorio;
            _movimentacaoServicos = movimentacaoServicos;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimentacaoViewModel>>> ObterMovimentacoes()
        {          
            try
            {
                var movimentacoes = await _movimentacaoRepositorio.ObterTodasMovimentacoes();
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<MovimentacaoViewModel>>(movimentacoes));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro interno ao processar a requisição: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MovimentacaoViewModel>> AdicionarMovimentacao(MovimentacaoCreateViewModel movimentacaoViewModel)
        {
            try
            {
                var movimentacao = _mapper.Map<Movimentacao>(movimentacaoViewModel);
                await _movimentacaoServicos.RegistrarMovimentacao(movimentacao);

                if (!OperacaoValida()) return CustomResponse(HttpStatusCode.BadRequest);

                return CustomResponse(HttpStatusCode.Created, _mapper.Map<MovimentacaoViewModel>(movimentacao));
            }
            catch (TratamentoExcecao ex) { NotificarErro(ex.Message.RemoveAccents()); return CustomResponse(HttpStatusCode.BadRequest);}
            catch (Exception ex){ NotificarErro("Erro ao processar movimentação: " + ex.Message.RemoveAccents()); return CustomResponse(HttpStatusCode.InternalServerError); }
        }

        [HttpGet("produto/{id:guid}")]
        public async Task<ActionResult<IEnumerable<MovimentacaoViewModel>>> ObterMovimentacaoPorProduto(Guid id)
        {
            try
            {
                var movimentacoes = await _movimentacaoRepositorio.ObterMovimentacoesPorProduto(id);
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<MovimentacaoViewModel>>(movimentacoes));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao buscar movimentações do produto: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("empresa/{id:guid}")]
        public async Task<ActionResult<IEnumerable<MovimentacaoViewModel>>> ObterMovimentacaoPorEmpresa(Guid id)
        {
            try
            {
                var movimentacoes = await _movimentacaoRepositorio.ObterMovimentacoesPorEmpresa(id);
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<MovimentacaoViewModel>>(movimentacoes));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao buscar movimentações da empresa: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("filial/{id:guid}")]
        public async Task<ActionResult<IEnumerable<MovimentacaoViewModel>>> ObterMovimentacaoPorFilial(Guid id)
        {
            try
            {
                var movimentacoes = await _movimentacaoRepositorio.ObterMovimentacoesPorFilial(id);
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<MovimentacaoViewModel>>(movimentacoes));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao buscar movimentações da filial: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
