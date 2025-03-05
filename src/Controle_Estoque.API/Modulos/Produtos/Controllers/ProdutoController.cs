using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Produtos.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Produtos;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Domain.Interfaces.Produtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_Estoque.API.Modulos.Produtos.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutoController : BasicController
    {

        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IProdutoServico _produtoServico;
        private readonly IMapper _mapper;


        public ProdutoController(IProdutoRepositorio produtoRepositorio, IProdutoServico produtoServico,
            IMapper mapper, INotificador notificador) : base(notificador)
        {
            _produtoRepositorio = produtoRepositorio;
            _produtoServico = produtoServico;
            _mapper = mapper;

        }


        [HttpGet]
        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos() //retornar o resultado do repositorio
        {
            var _produtos = await _produtoRepositorio.ObterProdutosComEmpresas();
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(_produtos);
        }


        // fazer trazer as suas filiais também
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid id)
        {
            var _produtoViewModel = await ObterProduto(id);

            if (_produtoViewModel == null) return NotFound();

            return _produtoViewModel;

        }



        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoCreateViewModel produtoCreateViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var _produto = _mapper.Map<Produto>(produtoCreateViewModel);
            await _produtoServico.Adicionar(_produto);

            return CustomResponse(HttpStatusCode.Created, _mapper.Map<ProdutoViewModel>(_produto));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id)
            {
                NotificarErro("Os ids informados não são iguais!");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var _produtoAtualizacao = await ObterProduto(id);


            _produtoAtualizacao.EmpresaId = produtoViewModel.EmpresaId;
            _produtoAtualizacao.FilialId = produtoViewModel.FilialId;
            _produtoAtualizacao.Nome = produtoViewModel.Nome;
            _produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            _produtoAtualizacao.Preco = produtoViewModel.Preco;
            _produtoAtualizacao.Ativo = produtoViewModel.Ativo;
           

            await _produtoServico.Atualizar(_mapper.Map<Produto>(_produtoAtualizacao));

            return CustomResponse(HttpStatusCode.NoContent);

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid id)
        {
            var _produto = await ObterProduto(id);

            if (_produto == null) return NotFound();

            await _produtoServico.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);

        }



        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterPorId(id));
        }


    }
}
