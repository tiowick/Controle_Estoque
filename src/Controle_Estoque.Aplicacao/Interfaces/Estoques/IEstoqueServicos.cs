using Controle_Estoque.Domain.Entidades.Estoques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Aplicacao.Interfaces.Estoques
{
    public interface IEstoqueServicos : IDisposable
    {
        Task Adicionar(Estoque estoque);
        Task AtualizarQuantidade(Estoque estoque);
        Task ProcessarMovimentacao(Guid produtoId, int quantidade, bool isEntrada);
        Task Remover(Guid id);
        
    }
}
