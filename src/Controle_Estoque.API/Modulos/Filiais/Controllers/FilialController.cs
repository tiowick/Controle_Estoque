using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Filiais.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Filiais;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Interfaces.Filiais;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Entidades.Validacoes.Documento.Padronizar.Texto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_Estoque.API.Modulos.Filiais.Controllers
{
    [ApiController]
    [Route("api/filiais")]
    public class FilialController : BasicController
    {
        private readonly IFilialRepositorio _filialRepositorio;
        private readonly IFilialServicos _filialServicos;
        private readonly IMapper _mapper;

        public FilialController(IFilialRepositorio filialRepositorio,
            IFilialServicos filialServicos, 
            IMapper mapper, 
            INotificador notificador) : base(notificador)
        {
            _filialRepositorio = filialRepositorio;
            _filialServicos = filialServicos;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilialViewModel>>> ObterFiliais()
        {
            try
            {
                var filiais = await _filialRepositorio.ObterFiliaisComEmpresa();
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<FilialViewModel>>(filiais));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao obter filiais: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FilialViewModel>> ObterPorId(Guid id)
        {
            try
            {
                var filialViewModel = await ObterFilial(id);
                if (filialViewModel == null) return NotFound();

                return CustomResponse(HttpStatusCode.OK, filialViewModel);
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao obter filial: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<FilialViewModel>> Adicionar(FilialCreateViewModel filialCreateViewModel)
        {
            try
            {
                var filial = _mapper.Map<Filial>(filialCreateViewModel);
                await _filialServicos.Adicionar(filial);

                return CustomResponse(HttpStatusCode.Created, _mapper.Map<FilialViewModel>(filial));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao adicionar filial: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, FilialUpdateViewModel filialViewModel)
        {
            try
            {
                var filialAtualizada = _mapper.Map<Filial>(filialViewModel);
                filialAtualizada.Id = id;
                await _filialServicos.Atualizar(filialAtualizada);

                return CustomResponse(HttpStatusCode.NoContent);
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao atualizar filial: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FilialViewModel>> Excluir(Guid id)
        {
            try
            {
                var filial = await ObterFilial(id);
                if (filial == null) return NotFound();

                await _filialServicos.Remover(id);
                return CustomResponse(HttpStatusCode.NoContent);
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao excluir filial: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        private async Task<FilialViewModel> ObterFilial(Guid id)
        {
            return _mapper.Map<FilialViewModel>(await _filialRepositorio.ObterPorId(id));
        }
    }
}
