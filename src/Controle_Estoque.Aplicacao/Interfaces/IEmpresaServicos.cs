using Controle_Estoque.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Aplicacao.Interfaces
{
    public interface IEmpresaServicos : IDisposable
    {
        // Task adicionar
        // Task atualizar
        // task escluir

        Task Adicionar(Empresa empresa);

        Task Atualizar(Empresa empresa);

        Task Remover(Guid id);
    }
}
