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
        //Task<IEnumerable<Empresa>> ObterEmpresaPorId(Guid id);

        //Trazendo todas as filiais aempresa que ela pertence
        Task<IEnumerable<Empresa>> ObterFilialPorEmpresa(Guid filialId);

        // busca todas as empresas no banco de dados e inclui as filiais associadas a elas
        Task<IEnumerable<Empresa>> ObterFiliaisEmpresas();

        Task<Empresa> ObterFilialEmpresa(Guid id);





    }
}
