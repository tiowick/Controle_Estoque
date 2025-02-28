using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Empresas;
using Controle_Estoque.Domain.Entidades.Empresas;
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
        public async Task<IEnumerable<EmpresaCreateViewModel>> ObterTodos() //retornar o resultado do repositorio
        {
            return _mapper.Map<IEnumerable<EmpresaCreateViewModel>>(await _empresaRepositorio.ObterEmpresasComFiliais());


        }

        [HttpPost]
        public async Task<ActionResult<EmpresaCreateViewModel>> Adicionar(EmpresaCreateViewModel empresaCreateViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _empresaServicos.Adicionar(_mapper.Map<Empresa>(empresaCreateViewModel));

            return CustomResponse(HttpStatusCode.Created, empresaCreateViewModel);
        }



   


    }
}
