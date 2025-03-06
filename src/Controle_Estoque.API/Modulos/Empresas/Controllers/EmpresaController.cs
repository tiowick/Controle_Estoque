using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Filiais.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Empresas;
using Controle_Estoque.Aplicacao.Interfaces.Produtos;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Produtos;
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
        public async Task<IEnumerable<EmpresaViewModel>> ObterEmpresas() //retornar o resultado do repositorio
        {
            var empresas = await _empresaRepositorio.ObterEmpresasComFiliais();
            return _mapper.Map<IEnumerable<EmpresaViewModel>>(empresas);
        }


        
        // fazer trazer as suas filiais também
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmpresaViewModel>> ObterPorId(Guid id)
        {
            var EmpresaViewModel = await ObterEmpresa(id);

            if (EmpresaViewModel == null) return NotFound();

            return EmpresaViewModel;

        }

        [HttpPost]
        public async Task<ActionResult<EmpresaViewModel>> Adicionar(EmpresaCreateViewModel empresaCreateViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var empresa = _mapper.Map<Empresa>(empresaCreateViewModel);
            await _empresaServicos.Adicionar(empresa);

            return CustomResponse(HttpStatusCode.Created, _mapper.Map<EmpresaViewModel>(empresa));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, EmpresaViewModel empresaViewModel)
        {
            if (id != empresaViewModel.Id)
            {
                NotificarErro("Os ids informados não são iguais!");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var _empresaAtualizada = _mapper.Map<Empresa>(empresaViewModel);
            await _empresaServicos.Atualizar(_empresaAtualizada);

            return CustomResponse(HttpStatusCode.NoContent);

        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EmpresaViewModel>> Excluir(Guid id)
        {
            var _empresa = await ObterEmpresa(id);

            if (_empresa == null) return NotFound();

            await _empresaServicos.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);

        }


        private async Task<EmpresaViewModel> ObterEmpresa(Guid id)
        {
            return _mapper.Map<EmpresaViewModel>(await _empresaRepositorio.ObterPorId(id));
        }



    }
}
