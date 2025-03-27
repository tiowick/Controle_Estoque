using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Filiais.ViewModels;
using Controle_Estoque.API.Modulos.Produtos.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Filiais;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Interfaces.Filiais;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;
using static Controle_Estoque.Domain.Enuns.IResponseController;
using JsonException = Newtonsoft.Json.JsonException;

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
        public async Task<ActionResult<IEnumerable<FilialViewModel>>> ObterFiliais() //retornar o resultado do repositorio
        {
            try
            {
                var _filial = await _filialRepositorio.ObterFiliaisComEmpresa();

                return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<FilialViewModel>>(_filial));
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }

        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FilialViewModel>> ObterPorId(Guid id)
        {
            try
            {
                var _filialViewModel = await ObterFilial(id);
                if (_filialViewModel == null) return NotFound();

                return _filialViewModel;
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }
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
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }

          
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, FilialUpdateViewModel filialViewModel)
        {

            try
            {

                var _filialAtualizada = _mapper.Map<Filial>(filialViewModel);

                _filialAtualizada.Id = id;
                await _filialServicos.Atualizar(_filialAtualizada);

                return CustomResponse(HttpStatusCode.NoContent);
            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }
        }



        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FilialViewModel>> Excluir(Guid id)
        {
            try
            {

                var _empresa = await ObterFilial(id);
                if (_empresa == null) return NotFound();

                await _filialServicos.Remover(id);

                return CustomResponse(HttpStatusCode.NoContent);

            }
            catch (TratamentoExcecao e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.BadRequest); }
            catch (Exception e) { NotificarErro(e.Message); return CustomResponse(HttpStatusCode.InternalServerError); }


        }

        private async Task<FilialViewModel> ObterFilial(Guid id)
        {
            return _mapper.Map<FilialViewModel>(await _filialRepositorio.ObterPorId(id));
        }


    

    }
}
