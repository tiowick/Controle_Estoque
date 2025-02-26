using Controle_Estoque.Domain.Entidades.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Aplicacao.Interfaces.Empresas
{
    public interface IEmpresaServicos : IDisposable
    {
        // empresa
        Task Adicionar(Empresa empresa);
        Task Atualizar(Empresa empresa);
        Task Remover(Guid id);

     
    }
}
