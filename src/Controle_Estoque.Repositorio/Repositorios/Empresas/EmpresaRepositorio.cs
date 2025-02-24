using Controle_Estoque.Domain.Entidades;
using Controle_Estoque.Domain.Interfaces.Empresas;
using Controle_Estoque.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Repositorio.Repositorios.Empresas
{
    public class EmpresaRepositorio : Repositorio<Empresa>, IEmpresaRepositorio
    {
        // criar chamada para AppDbContext

        public EmpresaRepositorio(AppDbContext context) : base(context)
        {

        }

        public Task<IEnumerable<Empresa>> ObterEmpresaPorId(Guid empresaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Empresa>> ObterFilialPorEmpresa()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Empresa>> ObterFilialPorId(Guid filialId)
        {
            throw new NotImplementedException();
        }
    }
}
