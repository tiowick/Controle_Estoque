using Controle_Estoque.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Interfaces.Empresas
{
    public interface IEmpresaRepositorio : IRepositorio<Empresa>
    {
        Task<IEnumerable<Empresa>> ObterEmpresaPorId(Guid empresaId);

        Task<IEnumerable<Empresa>> ObterFilialPorId(Guid filialId);

        Task<IEnumerable<Empresa>> ObterFilialPorEmpresa();

       



    }
}
