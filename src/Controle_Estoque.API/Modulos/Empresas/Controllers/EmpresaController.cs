using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Empresas;
using Controle_Estoque.Aplicacao.Interfaces.Produtos;
using Controle_Estoque.Domain.Entidades.Empresas;
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
        public async Task<IEnumerable<EmpresaCreateViewModel>> ObterEmpresas() //retornar o resultado do repositorio
        {
            return _mapper.Map<IEnumerable<EmpresaCreateViewModel>>(await _empresaRepositorio.ObterEmpresas());
        }

        //[HttpGet("filiais")]
        //public async Task<IEnumerable<EmpresaViewModel>> ObterEmpresasComFiliais()
        //{
        //    return _mapper.Map<IEnumerable<EmpresaViewModel>>(await _empresaRepositorio.ObterEmpresasComFiliais());
        //}


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmpresaCreateViewModel>> ObterPorId(Guid id)
        {
            var EmpresaViewModel = await ObterEmpresa(id);

            if (EmpresaViewModel == null) return NotFound();

            return EmpresaViewModel;

        }

        [HttpPost]
        public async Task<ActionResult<EmpresaCreateViewModel>> Adicionar(EmpresaCreateViewModel empresaCreateViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _empresaServicos.Adicionar(_mapper.Map<Empresa>(empresaCreateViewModel));

            return CustomResponse(HttpStatusCode.Created, empresaCreateViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, EmpresaCreateViewModel empresaCreateViewModel)
        {
            if (id != empresaCreateViewModel.Id)
            {
                NotificarErro("Os ids informados não são iguais!");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var _empresaAtualizacao = await ObterEmpresa(id);

            _empresaAtualizacao.Nome = empresaCreateViewModel.Nome;
            _empresaAtualizacao.Descricao = empresaCreateViewModel.Descricao;
            _empresaAtualizacao.CNPJ = empresaCreateViewModel.CNPJ;

            await _empresaServicos.Atualizar(_mapper.Map<Empresa>(_empresaAtualizacao)); 

            return CustomResponse(HttpStatusCode.NoContent);

        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EmpresaCreateViewModel>> Excluir(Guid id)
        {
            var _empresa = await ObterEmpresa(id);

            if (_empresa == null) return NotFound();

            await _empresaServicos.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);

        }


        private async Task<EmpresaCreateViewModel> ObterEmpresa(Guid id)
        {
            return _mapper.Map<EmpresaCreateViewModel>(await _empresaRepositorio.ObterPorId(id));
        }



    }
}
