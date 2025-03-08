using Controle_Estoque.Aplicacao.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Entidades.Validacoes;
using Controle_Estoque.Domain.Enuns;
using Controle_Estoque.Domain.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Domain.Interfaces.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Aplicacao.Servicos.Movimentacoes
{
    public class MovimentacaoServicos : BaseServices, IMovimentacaoServicos
    {
        private readonly IMovimentacaoRepositorio _movimentacaoRepositorio;

        public MovimentacaoServicos(IMovimentacaoRepositorio movimentacaoRepositorio, 
            INotificador notificador) : base(notificador)
        {
            _movimentacaoRepositorio = movimentacaoRepositorio;
        }


        public async Task RegistrarMovimentacao(Movimentacao movimentacao)
        {
            if (!ExecutarValidacao(new MovimentacaoValidacao(), movimentacao)) return;

            var movimentacaoExistente = await _movimentacaoRepositorio.ObterPorId(movimentacao.Id);
            if (movimentacaoExistente != null) return;     
 
            await _movimentacaoRepositorio.Adicionar(movimentacao);
        }


        public async Task<bool> ValidarMovimentacao(Movimentacao movimentacao)
        {
            if (movimentacao == null || movimentacao.ProdutoId == Guid.Empty || movimentacao.Quantidade <= 0)
            {
                Notificar("Movimentação inválida.");
                return false;
            }
            // Se for uma saída, verifica se há estoque suficiente
            if (movimentacao.TipoMovimentacao == IMovimentacao.saida)
            {
                var produto = await _movimentacaoRepositorio.ObterMovimentacaoPorId(movimentacao.ProdutoId);
                if (produto == null || produto.Quantidade < movimentacao.Quantidade)
                {
                    Notificar("Estoque insuficiente.");
                    return false;
                }
            }


            return true;
        }

        public void Dispose()
        {
            _movimentacaoRepositorio?.Dispose();
        }
    }
}
