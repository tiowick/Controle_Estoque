using Controle_Estoque.Domain.Entidades.Estoques;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Interfaces.Estoques;
using Controle_Estoque.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Repositorio.Repositorios.Estoques
{
    public class EstoqueRepositorio : Repositorio<Estoque>, IEstoqueRepositorio
    {

        public EstoqueRepositorio(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Estoque>> ObterEstoque()
        {
            return await Db.Estoques.ToListAsync();
        }

        public async Task<IEnumerable<Estoque>> ObterEstoquePorEmpresaId(Guid empresaId)
        {
            return await Buscar(e => e.EmpresaId == empresaId);
        }

        public async Task<IEnumerable<Estoque>> ObterEstoquePorFilialId(Guid filialId)
        {
           return await Buscar(e => e.FilialId == filialId);
        }

        public async Task<Estoque> ObterEstoquePorProdutoId(Guid produtoId)
        {
            return await Db.Estoques.AsNoTracking()
               .Include(e => e.Empresa)
               .FirstOrDefaultAsync(e => e.Id == produtoId);
        }
    }
}
