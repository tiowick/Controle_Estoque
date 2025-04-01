using Controle_Estoque.Aplicacao.Interfaces.Estoques;
using Controle_Estoque.Domain.Entidades.Estoques;
using Controle_Estoque.Domain.Entidades.Validacoes;
using Controle_Estoque.Domain.Interfaces.Estoques;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Domain.Entidades.Reflection;

namespace Controle_Estoque.Aplicacao.Servicos.Estoques
{
    public class EstoqueServicos : BaseServices, IEstoqueServicos
    {
        private readonly IEstoqueRepositorio _estoqueRepositorio;

        public EstoqueServicos(IEstoqueRepositorio estoqueRepositorio, 
            INotificador notificador) : base(notificador)
        {
            _estoqueRepositorio = estoqueRepositorio;
        }

        public async Task Adicionar(Estoque estoque)
        {
            try
            {
                if (!ExecutarValidacao(new EstoqueValidacao(), estoque)) return;

                var estoqueExistente = await _estoqueRepositorio.ObterEstoquePorProdutoId(estoque.ProdutoId);
                if (estoqueExistente != null)
                {
                    Notificar("Já existe um estoque cadastrado para este produto");
                    return;
                }

                await _estoqueRepositorio.Adicionar(estoque);
            }
            catch (Exception ex)
            {
                Notificar($"Erro ao adicionar estoque: {ex.Message}");
            }
        }

        public async Task AtualizarQuantidade(Estoque estoque)
        {
            try
            {
                var estoqueExistente = await _estoqueRepositorio.ObterEstoquePorProdutoId(estoque.ProdutoId);
                if (estoqueExistente == null)
                {
                    Notificar("Estoque não encontrado");
                    return;
                }

                if (estoque.Quantidade < 0)
                {
                    Notificar("A quantidade não pode ser negativa");
                    return;
                }

                estoqueExistente.Quantidade = estoque.Quantidade;
                estoqueExistente.DataAtualizacao = DateTime.Now;

                if (!ExecutarValidacao(new EstoqueValidacao(), estoqueExistente)) return;

                await _estoqueRepositorio.Atualizar(estoqueExistente);
            }
            catch (Exception ex)
            {
                Notificar($"Erro ao atualizar quantidade em estoque: {ex.Message}");
            }
        }

        public async Task ProcessarMovimentacao(Guid produtoId, int quantidade, bool isEntrada)
        {
            try
            {
                var estoque = await _estoqueRepositorio.ObterEstoquePorProdutoId(produtoId);
                if (estoque == null)
                {
                    Notificar("Estoque não encontrado para este produto");
                    return;
                }

                if (!isEntrada && estoque.Quantidade < quantidade)
                {
                    Notificar("Quantidade insuficiente em estoque");
                    return;
                }

                estoque.Quantidade = isEntrada ?  estoque.Quantidade + quantidade : estoque.Quantidade - quantidade;
                
                estoque.DataAtualizacao = DateTime.Now;

                if (!ExecutarValidacao(new EstoqueValidacao(), estoque)) return;

                await _estoqueRepositorio.Atualizar(estoque);
            }
            catch (Exception ex)
            {
                Notificar($"Erro ao processar movimentação no estoque: {ex.Message}");
                throw new TratamentoExcecao(ex.Message);
            }
        }

        public async Task Remover(Guid id)
        {
            try
            {
                var estoque = await _estoqueRepositorio.ObterPorId(id);
                if (estoque == null)
                {
                    Notificar("Estoque não encontrado");
                    return;
                }

                await _estoqueRepositorio.Remover(id);
            }
            catch (Exception ex)
            {
                Notificar($"Erro ao remover estoque: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _estoqueRepositorio?.Dispose();
        }
    }
}
