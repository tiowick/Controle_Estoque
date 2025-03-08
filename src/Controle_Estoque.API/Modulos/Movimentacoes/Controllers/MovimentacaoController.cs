using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Movimentacoes.ViewModels;
using Controle_Estoque.API.Modulos.Produtos.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Interfaces.Notificador;
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
        private IMapper _mapper;

        public MovimentacaoController(IMovimentacaoRepositorio movimentacaoRepositorio, 
            IMovimentacaoServicos movimentacaoServicos, IMapper mapper, INotificador notificador) : base(notificador)
        {
            _movimentacaoRepositorio = movimentacaoRepositorio;
            _movimentacaoServicos = movimentacaoServicos;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IEnumerable<MovimentacaoViewModel>> ObterMovimentacoes() //retornar o resultado do repositorio
        {
            var _movimentacao = await _movimentacaoRepositorio.ObterTodasMovimentacoes();
            return _mapper.Map<IEnumerable<MovimentacaoViewModel>>(_movimentacao);
        }


        [HttpPost]
        public async Task<ActionResult<MovimentacaoViewModel>> AdicionarMovimentacao(MovimentacaoCreateViewModel movimentacaoCreateViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var _movimentacao = _mapper.Map<Movimentacao>(movimentacaoCreateViewModel);
            await _movimentacaoServicos.RegistrarMovimentacao(_movimentacao);

            return CustomResponse(HttpStatusCode.Created, _mapper.Map<MovimentacaoViewModel>(_movimentacao));
        }

        [HttpGet("produto/{id:guid}")] // pegar as movimentações por produto
        public async Task<IEnumerable<MovimentacaoViewModel>> ObterMovimentacaoPorProduto(Guid id)
        {
            var _movimentacao = await _movimentacaoRepositorio.ObterMovimentacoesPorProduto(id);
            return _mapper.Map<IEnumerable<MovimentacaoViewModel>>(_movimentacao);
        }

        [HttpGet("empresa/{id:guid}")] // pegar as movimentações por empresa
        public async Task<IEnumerable<MovimentacaoViewModel>> ObterMovimentacaoPorEmpresa(Guid id)
        {
            var _movimentacao = await _movimentacaoRepositorio.ObterMovimentacoesPorEmpresa(id);
            return _mapper.Map<IEnumerable<MovimentacaoViewModel>>(_movimentacao);
        }



    }
}
