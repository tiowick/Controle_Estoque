using Controle_Estoque.Domain.Entidades.Filiais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Aplicacao.Interfaces.Filiais
{
    public interface IFilialServicos : IDisposable
    {
        // filial
        Task Adicionar(Filial filial);
        Task Atualizar(Filial filial);
        Task Remover(Guid id);
    }
}
