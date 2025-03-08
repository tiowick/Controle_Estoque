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
        Task<Estoque> ObterEstoquePorProdutoId(Guid produtoId);

        // Busca todos os registros de estoque de uma empresa
        Task<IEnumerable<Estoque>> ObterEstoquePorEmpresaId(Guid empresaId);

        // Busca todos os registros de estoque de uma filial específica
        Task<IEnumerable<Estoque>> ObterEstoquePorFilialId(Guid filialId);

        Task<IEnumerable<Estoque>> ObterEstoque();




    }
}
