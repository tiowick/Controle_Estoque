using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Filiais.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Filiais;
using Controle_Estoque.Domain.Interfaces.Filiais;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Microsoft.AspNetCore.Mvc;

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

    }
}
