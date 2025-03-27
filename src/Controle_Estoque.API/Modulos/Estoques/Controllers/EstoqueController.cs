using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.Aplicacao.Interfaces.Estoques;
using Controle_Estoque.Domain.Interfaces.Estoques;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Microsoft.AspNetCore.Mvc;

namespace Controle_Estoque.API.Modulos.Estoques.Controllers
{
    [ApiController]
    [Route("api/estoque")]
    public class EstoqueController : BasicController
    {

        private readonly IEstoqueRepositorio _estoqueRepositorio;
        private readonly IEstoqueServicos _estoqueServicos;
        private readonly IMapper _mapper;


        public EstoqueController(IEstoqueRepositorio estoqueRepositorio, IEstoqueServicos estoqueServicos
            , IMapper mapper, INotificador notificador) : base(notificador)
        {
            _estoqueRepositorio = estoqueRepositorio;
            _estoqueServicos = estoqueServicos;
            _mapper = mapper;
        }




    }
}
