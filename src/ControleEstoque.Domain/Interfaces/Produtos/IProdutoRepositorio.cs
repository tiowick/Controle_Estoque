using Controle_Estoque.Domain.Entidades.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Interfaces.Produtos
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {

        //aqui to buscando Produtos por empresa
        Task<IEnumerable<Produto>> ObterProdutosPorEmpresa(Guid empresaId);

        // Aqui to buscando Produtos com suas empresas
        Task<IEnumerable<Produto>> ObterProdutos();

        // Aqui to buscando produtos especifico
        Task<Produto> ObterProdutoPorIdComEmpresa(Guid id);

        Task<IEnumerable<Produto>> ObterProdutosPorFilial(Guid filialId);

        Task<IEnumerable<Produto>> ObterProdutosComFiliais();

        Task<Produto> ObterProdutoPorIdComFilial(Guid id);
        
    }
}
