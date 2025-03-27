using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Filiais.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Empresas;
using Controle_Estoque.Aplicacao.Interfaces.Produtos;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Interfaces.Empresas;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Microsoft.AspNetCore.Authorization;
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

        public EmpresaController(IEmpresaRepositorio empresaRepositorio, IEmpresaServicos empresaServicos,
            IMapper mapper, INotificador notificador) : base(notificador)
        {
            _empresaRepositorio = empresaRepositorio;
            _empresaServicos = empresaServicos;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaViewModel>>> ObterEmpresas() //retornar o resultado do repositorio
        {

            try
            {

                var _empresas = await _empresaRepositorio.ObterEmpresasComFiliais();
                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<EmpresaViewModel>>(_empresas)); ;
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }

        }

        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmpresaViewModel>> ObterPorId(Guid id)
        {

            try
            {
                var _empresaViewModel = await ObterEmpresa(id);
                if (_empresaViewModel == null) return NotFound();

                return Ok(_empresaViewModel);
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }
           
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
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }
            
        }



        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, EmpresaViewModel empresaViewModel)
        {

            try
            {

                var _empresaAtualizada = _mapper.Map<Empresa>(empresaViewModel);
                _empresaAtualizada.Id = id;
                await _empresaServicos.Atualizar(_empresaAtualizada);

                return CustomResponse(HttpStatusCode.NoContent);
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EmpresaViewModel>> Excluir(Guid id)
        {

            try
            {

                var _empresa = await ObterEmpresa(id);
                if (_empresa == null) return NotFound();
                await _empresaServicos.Remover(id);

                return CustomResponse(HttpStatusCode.NoContent);
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }

            

        }


        private async Task<EmpresaViewModel> ObterEmpresa(Guid id)
        {
            return _mapper.Map<EmpresaViewModel>(await _empresaRepositorio.ObterPorId(id));
        }



    }
}
