using Controle_Estoque.Aplicacao.Interfaces.Produtos;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Entidades.Validacoes;
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
            if (!ExecutarValidacao(new ProdutoValidacao(), produto)) return;

            var produtoExistente = _produtoRepositorio.ObterPorId(produto.Id);
            if (produtoExistente != null)
            {
                Notificar("Ja existe um produto com ID informado!");
                return;
            }

            await _produtoRepositorio.Adicionar(produto); ;
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidacao(), produto)) return;

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
