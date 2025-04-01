using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Estoques.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Estoques;
using Controle_Estoque.Domain.Entidades.Estoques;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Interfaces.Estoques;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Entidades.Validacoes.Documento.Padronizar.Texto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_Estoque.API.Modulos.Estoques.Controllers
{
    [ApiController]
    [Route("api/estoque")]
    public class EstoqueController : BasicController
    {
        private readonly IEstoqueRepositorio _estoqueRepositorio;
        private readonly IEstoqueServicos _estoqueServicos;
        private readonly IMapper _mapper;

        public EstoqueController(IEstoqueRepositorio estoqueRepositorio, 
            IEstoqueServicos estoqueServicos,
            IMapper mapper, 
            INotificador notificador) : base(notificador)
        {
            _estoqueRepositorio = estoqueRepositorio;
            _estoqueServicos = estoqueServicos;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstoqueViewModel>>> ObterEstoque()
        {
            try
            {
                var estoque = await _estoqueRepositorio.ObterEstoque();
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<EstoqueViewModel>>(estoque));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao obter estoque: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("produto/{id:guid}")]
        public async Task<ActionResult<EstoqueViewModel>> ObterEstoquePorProduto(Guid id)
        {
            try
            {
                var estoque = await _estoqueRepositorio.ObterEstoquePorProdutoId(id);
                if (estoque == null) return NotFound();

                return CustomResponse(HttpStatusCode.OK, _mapper.Map<EstoqueViewModel>(estoque));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao obter estoque do produto: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("empresa/{id:guid}")]
        public async Task<ActionResult<IEnumerable<EstoqueViewModel>>> ObterEstoquePorEmpresa(Guid id)
        {
            try
            {
                var estoque = await _estoqueRepositorio.ObterEstoquePorEmpresaId(id);
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<EstoqueViewModel>>(estoque));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao obter estoque da empresa: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("filial/{id:guid}")]
        public async Task<ActionResult<IEnumerable<EstoqueViewModel>>> ObterEstoquePorFilial(Guid id)
        {
            try
            {
                var estoque = await _estoqueRepositorio.ObterEstoquePorFilialId(id);
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<EstoqueViewModel>>(estoque));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao obter estoque da filial: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EstoqueViewModel>> AdicionarEstoque(EstoqueCreateViewModel estoqueCreateViewModel)
        {
            try
            {
                var estoque = _mapper.Map<Estoque>(estoqueCreateViewModel);
                await _estoqueServicos.Adicionar(estoque);

                if (!OperacaoValida()) return CustomResponse(HttpStatusCode.BadRequest);

                return CustomResponse(HttpStatusCode.Created, _mapper.Map<EstoqueViewModel>(estoque));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao adicionar estoque: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("atualizar-quantidade")]
        public async Task<ActionResult> AtualizarQuantidade(EstoqueUpdateViewModel estoqueUpdateViewModel)
        {
            try
            {
                var estoque = _mapper.Map<Estoque>(estoqueUpdateViewModel);
                await _estoqueServicos.AtualizarQuantidade(estoque);

                if (!OperacaoValida()) return CustomResponse(HttpStatusCode.BadRequest);

                return CustomResponse(HttpStatusCode.NoContent);
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao atualizar quantidade em estoque: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
