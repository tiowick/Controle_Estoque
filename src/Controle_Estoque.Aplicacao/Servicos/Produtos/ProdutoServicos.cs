using Controle_Estoque.Aplicacao.Interfaces.Produtos;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Entidades.Validacoes;
using Controle_Estoque.Domain.Interfaces.Filiais;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Domain.Interfaces.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Aplicacao.Servicos.Produtos
{
    public class ProdutoServicos : BaseServices, IProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoServicos(IProdutoRepositorio produtoRepositorio,
            INotificador notificador) : base(notificador)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task Adicionar(Produto produto)
        {
            var resultado = await new ProdutoValidacao(_produtoRepositorio).ValidateAsync(produto);
            if (!resultado.IsValid)
            {
                resultado.Errors.ForEach(e => Notificar(e.ErrorMessage));
                return;
            }
            await _produtoRepositorio.Adicionar(produto); 
        }

        public async Task Atualizar(Produto produto)
        {
            var resultado = await new ProdutoValidacao(_produtoRepositorio).ValidateAsync(produto);
            if (!resultado.IsValid)
            {
                resultado.Errors.ForEach(e => Notificar(e.ErrorMessage));
                return;
            }

            await _produtoRepositorio.Atualizar(produto);
        }

        public async Task Remover(Guid id)
        {
            await _produtoRepositorio.Remover(id); ;
        }

        public void Dispose()
        {
            _produtoRepositorio?.Dispose();
        }
    }
}
