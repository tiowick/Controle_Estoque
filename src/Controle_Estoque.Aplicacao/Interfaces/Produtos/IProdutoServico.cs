using Controle_Estoque.Domain.Entidades.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Aplicacao.Interfaces.Produtos
{
    public interface IProdutoServico : IDisposable
    {
        // produto
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}
