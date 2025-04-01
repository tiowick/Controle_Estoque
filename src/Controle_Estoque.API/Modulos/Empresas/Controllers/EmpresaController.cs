using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Empresas;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Interfaces.Empresas;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Entidades.Validacoes.Documento.Padronizar.Texto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_Estoque.API.Modulos.Empresas.Controllers
{
    [ApiController]
    [Route("api/empresas")]
    public class EmpresaController : BasicController
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IEmpresaServicos _empresaServicos;
        private readonly IMapper _mapper;

        public EmpresaController(IEmpresaRepositorio empresaRepositorio, 
            IEmpresaServicos empresaServicos,
            IMapper mapper, 
            INotificador notificador) : base(notificador)
        {
            _empresaRepositorio = empresaRepositorio;
            _empresaServicos = empresaServicos;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaViewModel>>> ObterEmpresas()
        {
            try
            {
                var empresas = await _empresaRepositorio.ObterEmpresasComFiliais();
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<EmpresaViewModel>>(empresas));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao obter empresas: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmpresaViewModel>> ObterPorId(Guid id)
        {
            try
            {
                var empresaViewModel = await ObterEmpresa(id);
                if (empresaViewModel == null) return NotFound();

                return CustomResponse(HttpStatusCode.OK, empresaViewModel);
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao obter empresa: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmpresaViewModel>> Adicionar(EmpresaCreateViewModel empresaCreateViewModel)
        {
            try
            {
                var empresa = _mapper.Map<Empresa>(empresaCreateViewModel);
                await _empresaServicos.Adicionar(empresa);

                return CustomResponse(HttpStatusCode.Created, _mapper.Map<EmpresaViewModel>(empresa));
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao adicionar empresa: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, EmpresaViewModel empresaViewModel)
        {
            try
            {
                var empresaAtualizada = _mapper.Map<Empresa>(empresaViewModel);
                empresaAtualizada.Id = id;
                await _empresaServicos.Atualizar(empresaAtualizada);

                return CustomResponse(HttpStatusCode.NoContent);
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao atualizar empresa: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EmpresaViewModel>> Excluir(Guid id)
        {
            try
            {
                var empresa = await ObterEmpresa(id);
                if (empresa == null) return NotFound();

                await _empresaServicos.Remover(id);
                return CustomResponse(HttpStatusCode.NoContent);
            }
            catch (TratamentoExcecao ex)
            {
                NotificarErro(ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                NotificarErro("Erro ao excluir empresa: " + ex.Message.RemoveAccents());
                return CustomResponse(HttpStatusCode.InternalServerError);
            }
        }

        private async Task<EmpresaViewModel> ObterEmpresa(Guid id)
        {
            return _mapper.Map<EmpresaViewModel>(await _empresaRepositorio.ObterPorId(id));
        }
    }
}
