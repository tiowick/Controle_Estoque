using AutoMapper;
using Controle_Estoque.API.Controllers;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Produtos.ViewModels;
using Controle_Estoque.Aplicacao.Interfaces.Produtos;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Domain.Interfaces.Produtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_Estoque.API.Modulos.Produtos.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutoController : BasicController
    {

        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IProdutoServico _produtoServico;
        private readonly IMapper _mapper;


        public ProdutoController(IProdutoRepositorio produtoRepositorio, IProdutoServico produtoServico,
            IMapper mapper, INotificador notificador) : base(notificador)
        {
            _produtoRepositorio = produtoRepositorio;
            _produtoServico = produtoServico;
            _mapper = mapper;

        }



        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoCreateViewModel produtoCreateViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var _produto = _mapper.Map<Produto>(produtoCreateViewModel);
            await _produtoServico.Adicionar(_produto);

            return CustomResponse(HttpStatusCode.Created, _mapper.Map<ProdutoViewModel>(_produto));
        }






    }
}
