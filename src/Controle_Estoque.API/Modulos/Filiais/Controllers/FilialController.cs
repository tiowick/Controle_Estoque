using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Filiais.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Filiais;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Interfaces.Filiais;
using Controle_Estoque.Domain.Interfaces.Notificador;
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
            IFilialServicos filialServicos, IMapper mapper, INotificador notificador) : base(notificador)
        {
            _filialRepositorio = filialRepositorio;
            _filialServicos = filialServicos;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<FilialCreateViewModel>> ObterFiliais() //retornar o resultado do repositorio
        {
            return _mapper.Map<IEnumerable<FilialCreateViewModel>>(await _filialRepositorio.Obterfiliais());

        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FilialCreateViewModel>> ObterPorId(Guid id)
        {
            var _filialViewModel = await ObterFilial(id);

            if (_filialViewModel == null) return NotFound();

            return _filialViewModel;

        }


        [HttpPost]
        public async Task<ActionResult<FilialCreateViewModel>> Adicionar(FilialCreateViewModel filialCreateViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _filialServicos.Adicionar(_mapper.Map<Filial>(filialCreateViewModel));

            return CustomResponse(HttpStatusCode.Created, filialCreateViewModel);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, FilialCreateViewModel filialCreateViewModel)
        {
            if (id != filialCreateViewModel.Id)
            {
                NotificarErro("Os ids informados não são iguais!");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var _filialAtualizacao = await ObterFilial(id);


            _filialAtualizacao.EmpresaId = filialCreateViewModel.EmpresaId;
            _filialAtualizacao.Nome = filialCreateViewModel.Nome;
            _filialAtualizacao.Descricao = filialCreateViewModel.Descricao;
            _filialAtualizacao.CNPJ = filialCreateViewModel.CNPJ;

            await _filialServicos.Atualizar(_mapper.Map<Filial>(_filialAtualizacao));

            return CustomResponse(HttpStatusCode.NoContent);

        }



        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FilialCreateViewModel>> Excluir(Guid id)
        {
            var _empresa = await ObterFilial(id);

            if (_empresa == null) return NotFound();

            await _filialServicos.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);

        }



        private async Task<FilialCreateViewModel> ObterFilial(Guid id)
        {
            return _mapper.Map<FilialCreateViewModel>(await _filialRepositorio.ObterPorId(id));
        }


    

    }
}
