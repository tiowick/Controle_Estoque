using Controle_Estoque.Domain.Entidades;
using Controle_Estoque.Domain.Interfaces.Filiais;
using Controle_Estoque.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Repositorio.Repositorios.Filiais
{
    public class FilialRepositorio : Repositorio<Filial>, IFilialRepositorio
    {

        // criar chamada para AppDbContext

        public FilialRepositorio(AppDbContext context) : base(context)
        {

        }

        public async Task<Filial> ObterFilialPorId(Guid id)
        {
            return await ObterPorId(id);

        }

        public async Task<IEnumerable<Filial>> ObterFiliaisComEmpresa()
        {
            return await Db.Filiais.AsNoTracking()
                .Include(f => f.Nome)
                .OrderBy(e => e.Empresa)
                .ToListAsync();
        }

        public async Task<IEnumerable<Filial>> ObterFiliaisPorEmpresa(Guid empresaId)
        {
            return await Buscar(e => e.EmpresaId == empresaId);
        }
    }
}
