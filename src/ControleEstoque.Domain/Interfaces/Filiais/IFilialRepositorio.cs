using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Interfaces.Filiais
{
    public interface IFilialRepositorio : IRepositorio<Filial>
    {
        Task<IEnumerable<Filial>> Obterfiliais();

        // Obtém uma filial pelo ID
        Task<Filial> ObterFilialPorId(Guid id);

        // Busca todas as empresas e inclui suas filiais associadas
        Task<IEnumerable<Filial>> ObterFiliaisComEmpresa();

        // Obtém todas as filiais de uma empresa específica
        Task<IEnumerable<Filial>> ObterFiliaisPorEmpresa(Guid empresaId);

    }
}
