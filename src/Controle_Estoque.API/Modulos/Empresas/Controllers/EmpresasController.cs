using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Empresas;
using Controle_Estoque.Domain.Interfaces.Empresas;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controle_Estoque.API.Modulos.Empresas.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/empresas")]
    public class EmpresasController : BasicController
    {


        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IEmpresaServicos _empresaServicos;
        private readonly IMapper _mapper;

        public EmpresasController(IEmpresaRepositorio empresaRepositorio, IEmpresaServicos empresaServicos,
            IMapper mapper, INotificador notificador) : base(notificador)
        {
            _empresaRepositorio = empresaRepositorio;
            _empresaServicos = empresaServicos;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IEnumerable<EmpresaViewModel>> ObterTodos() //retornar o resultado do repositorio
        {
            return _mapper.Map<IEnumerable<EmpresaViewModel>>(await _empresaRepositorio.ObterFiliaisEmpresas());


        }

        // var jeferson = await Wick7(j == j.Jeferson).AsNoTracking()
        //          .Antes de vim pra cá, criar todas as regras das entidades
        //          .Quando tiver tudo sólido, vem pra essa parte
        //          .ToListAsync();

   


    }
}
