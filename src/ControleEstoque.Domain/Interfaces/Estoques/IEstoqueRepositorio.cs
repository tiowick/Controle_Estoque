using Controle_Estoque.Domain.Entidades.Estoques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Interfaces.Estoques
{
    public interface IEstoqueRepositorio : IRepositorio<Estoque>
    {
        // Busca o registro de estoque para um produto específico
        Task<Estoque> ObterPorProdutoId(Guid produtoId);

        // Busca todos os registros de estoque de uma empresa
        Task<IEnumerable<Estoque>> ObterPorEmpresaId(Guid empresaId);

        // Busca todos os registros de estoque de uma filial específica
        Task<IEnumerable<Estoque>> ObterPorFilialId(Guid filialId);
    }
}
