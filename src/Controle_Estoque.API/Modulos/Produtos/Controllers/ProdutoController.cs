using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Produtos.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Produtos;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Domain.Interfaces.Produtos;
using Controle_Estoque.Entidades.Validacoes.Documento.Padronizar.Texto;
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

        public ProdutoController(IProdutoRepositorio produtoRepositorio, 
            IProdutoServico produtoServico,
            IMapper mapper, 
            INotificador notificador) : base(notificador)
        {
            _produtoRepositorio = produtoRepositorio;
            _produtoServico = produtoServico;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> ObterTodos()
        {
            try
            {
                var produtos = await _produtoRepositorio.ObterProdutos();
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos));
            }
            catch (TratamentoExcecao ex) { NotificarErro(ex.Message.RemoveAccents()); return CustomResponse(HttpStatusCode.BadRequest);}
            catch (Exception ex) { NotificarErro("Erro ao obter produtos: " + ex.Message.RemoveAccents()); return CustomResponse(HttpStatusCode.InternalServerError);}
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid id)
        {
            try
            {
                var produtoViewModel = await ObterProduto(id);
                if (produtoViewModel == null) return NotFound();

                return CustomResponse(HttpStatusCode.OK, produtoViewModel);
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao obter produto: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoCreateViewModel produtoCreateViewModel)
        {
            try
            {
                var produto = _mapper.Map<Produto>(produtoCreateViewModel);
                await _produtoServico.Adicionar(produto);

                return CustomResponse(HttpStatusCode.Created, _mapper.Map<ProdutoViewModel>(produto));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao adicionar produto: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, ProdutoUpdateViewModel produtoViewModel)
        {
            try
            {
                var produtoAtualizado = _mapper.Map<Produto>(produtoViewModel);
                produtoAtualizado.Id = id;
                await _produtoServico.Atualizar(produtoAtualizado);

                return CustomResponse(HttpStatusCode.NoContent);
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao atualizar produto: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid id)
        {
            try
            {
                var produto = await ObterProduto(id);
                if (produto == null) return NotFound();

                await _produtoServico.Remover(id);
                return CustomResponse(HttpStatusCode.NoContent);
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao excluir produto: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterPorId(id));
        }
    }
}
