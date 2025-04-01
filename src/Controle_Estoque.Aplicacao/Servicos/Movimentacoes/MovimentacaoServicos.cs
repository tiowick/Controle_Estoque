using Controle_Estoque.Aplicacao.Interfaces.Estoques;
using Controle_Estoque.Aplicacao.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Entidades.Validacoes;
using Controle_Estoque.Domain.Enuns;
using Controle_Estoque.Domain.Interfaces.Movimentacoes;
using Controle_Estoque.Domain.Interfaces.Notificador;

namespace Controle_Estoque.Aplicacao.Servicos.Movimentacoes
{
    public class MovimentacaoServicos : BaseServices, IMovimentacaoServicos
    {
        private readonly IMovimentacaoRepositorio _movimentacaoRepositorio;
        private readonly IEstoqueServicos _estoqueServicos;

        public MovimentacaoServicos(IMovimentacaoRepositorio movimentacaoRepositorio,
            IEstoqueServicos estoqueServicos,
            INotificador notificador) : base(notificador)
        {
            _movimentacaoRepositorio = movimentacaoRepositorio;
            _estoqueServicos = estoqueServicos;
        }

        public async Task RegistrarMovimentacao(Movimentacao movimentacao)
        {
            try
            {
                if (!ExecutarValidacao(new MovimentacaoValidacao(), movimentacao))
                    return;

                if (movimentacao.Quantidade <= 0)
                {
                    Notificar("A quantidade da movimentação deve ser maior que zero");
                    return;
                }

                // Processa a movimentação no estoque
                bool isEntrada = movimentacao.TipoMovimentacao == TiposMovimentacoes.entrada;
                
                try 
                {
                    await _estoqueServicos.ProcessarMovimentacao(movimentacao.ProdutoId, movimentacao.Quantidade, isEntrada);
                }
                catch (TratamentoExcecao ex) { Notificar(ex.Message);return; }

                // Registra a movimentação
                movimentacao.DataMovimentacao = DateTime.Now;
                await _movimentacaoRepositorio.Adicionar(movimentacao);
            }
            catch (TratamentoExcecao ex){ Notificar(ex.Message);}
            catch (Exception ex) { Notificar($"Erro ao registrar movimentação: {ex.Message}"); }
        }

        public void Dispose()
        {
            _movimentacaoRepositorio?.Dispose();
            _estoqueServicos?.Dispose();
        }
    }
}
