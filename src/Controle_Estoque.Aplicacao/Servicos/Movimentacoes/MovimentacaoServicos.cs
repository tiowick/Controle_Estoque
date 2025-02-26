using Controle_Estoque.Aplicacao.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Movimentacoes;
using Controle_Estoque.Domain.Enuns;
using Controle_Estoque.Domain.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Interfaces.Notificador;
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


        public async Task<bool> RegistrarMovimentacao(Movimentacao movimentacao)
        {
            if (!await ValidarMovimentacao(movimentacao)) return false;

            // Adiciona a movimentação
            await _movimentacaoRepositorio.Adicionar(movimentacao);

            var produto = await _movimentacaoRepositorio.ObterMovimentacaoPorId(movimentacao.ProdutoId);
            if (produto == null) return false;

            produto.Quantidade += movimentacao.TipoMovimentacao == IMovimentacao.entrada
                ? movimentacao.Quantidade
                : -movimentacao.Quantidade;

            await _movimentacaoRepositorio.Atualizar(produto);

            return true;
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
