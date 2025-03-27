using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Movimentacoes.ViewModels;
using Controle_Estoque.API.Modulos.Produtos.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Enuns;
using Controle_Estoque.Domain.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Domain.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Controle_Estoque.API.Modulos.Movimentacoes.Controllers
{

    [ApiController]
    [Route("api/movimentacoes")]
    public class MovimentacaoController : BasicController
    {
        private readonly IMovimentacaoRepositorio _movimentacaoRepositorio;
        private readonly IMovimentacaoServicos _movimentacaoServicos;
        private IMapper _mapper;

        public MovimentacaoController(IMovimentacaoRepositorio movimentacaoRepositorio, 
            IMovimentacaoServicos movimentacaoServicos, IMapper mapper, INotificador notificador) : base(notificador)
        {
            _movimentacaoRepositorio = movimentacaoRepositorio;
            _movimentacaoServicos = movimentacaoServicos;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimentacaoViewModel>>> ObterMovimentacoes() //retornar o resultado do repositorio
        {          
            
            try
            {
                var _movimentacao = await _movimentacaoRepositorio.ObterTodasMovimentacoes();
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<MovimentacaoViewModel>>(_movimentacao));
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }
        }


        [HttpPost]
        public async Task<ActionResult<MovimentacaoViewModel>> AdicionarMovimentacao(MovimentacaoCreateViewModel movimentacaoCreateViewModel)
        {

            try
            {

                var _movimentacao = _mapper.Map<Movimentacao>(movimentacaoCreateViewModel);
                await _movimentacaoServicos.RegistrarMovimentacao(_movimentacao);

                return CustomResponse(HttpStatusCode.Created, _mapper.Map<MovimentacaoViewModel>(_movimentacao));
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }
        }


        [HttpGet("produto/{id:guid}")] // pegar as movimentações por produto
        public async Task<ActionResult<IEnumerable<MovimentacaoViewModel>>> ObterMovimentacaoPorProduto(Guid id)
        {
            try
            {
                var _movimentacaoProduto = await _movimentacaoRepositorio.ObterMovimentacoesPorProduto(id);
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<MovimentacaoViewModel>>(_movimentacaoProduto));
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }
            
        }

        [HttpGet("empresa/{id:guid}")] // pegar as movimentações por empresa
        public async Task<ActionResult<IEnumerable<MovimentacaoViewModel>>> ObterMovimentacaoPorEmpresa(Guid id)
        {

            try
            {
                var _movimentacaoEmpresa = await _movimentacaoRepositorio.ObterMovimentacoesPorEmpresa(id);
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<MovimentacaoViewModel>>(_movimentacaoEmpresa));
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }
        }

        [HttpGet("filial/{id:guid}")] // pegar as movimentações por filial
        public async Task<ActionResult<IEnumerable<MovimentacaoViewModel>>> ObterMovimentacaoPorFilial(Guid id)
        {
            try
            {
                var _movimentacaoFilial = await _movimentacaoRepositorio.ObterMovimentacoesPorFilial(id);
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<MovimentacaoViewModel>>(_movimentacaoFilial));
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }

            
        }

        [HttpGet("tipo/{tipo}")] // pegar as movimentações por tipo
        public async Task<ActionResult<IEnumerable<MovimentacaoViewModel>>> ObterMovimentacaoPorTipo(IMovimentacao tipo)
        {
            try
            {
                var _movimentacaoTipo = await _movimentacaoRepositorio.ObterMovimentacoesPorTipo(tipo);
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<MovimentacaoViewModel>>(_movimentacaoTipo));
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }
            
        }



    }
}
