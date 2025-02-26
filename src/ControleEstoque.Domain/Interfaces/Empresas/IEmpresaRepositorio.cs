using Controle_Estoque.Domain.Entidades.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Interfaces.Empresas
{
    public interface IEmpresaRepositorio : IRepositorio<Empresa>
    {
        Task<Empresa> ObterEmpresaPorId(Guid id);

        //Trazendo todas as filiais a empresa que ela pertence
        Task<IEnumerable<Empresa>> ObterFiliaisPorEmpresa(Guid filialId);

        // Busca todas as empresas e inclui suas filiais associadas
        Task<IEnumerable<Empresa>> ObterEmpresasComFiliais();

        Task<Empresa> ObterEmpresaPorIdComFiliais(Guid id);


    }
}
