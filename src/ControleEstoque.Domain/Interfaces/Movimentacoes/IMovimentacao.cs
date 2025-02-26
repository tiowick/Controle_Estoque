using Controle_Estoque.Domain.Entidades.Movimentacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Interfaces.Movimentacoes
{
    public interface IMovimentacao : IRepositorio<Movimentacao>
    {
        Task<IEnumerable<Movimentacao>> ObterTodasMovimentacoes();
        Task<Movimentacao> ObterMovimentacaoPorId(Guid id);
        Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorEmpresa(Guid empresaId);
        Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorFilial(Guid filialId);
        Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorProduto(Guid produtoId);
        Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorTipo(IMovimentacao tipo);
    }
}
