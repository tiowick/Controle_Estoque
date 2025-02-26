using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Interfaces.Produtos;
using Controle_Estoque.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Repositorio.Repositorios.Produtos
{
    public class ProdutoRepositorio : Repositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(AppDbContext context) : base(context)
        {

        }

        public async Task<Produto> ObterProdutoPorIdComEmpresa(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                .Include(e => e.Empresa)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Produto> ObterProdutoPorIdComFilial(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                .Include(e => e.Filial)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosComEmpresas()
        {
            return await Db.Produtos.AsNoTracking()
                .Include(p => p.Nome)
                .OrderBy(e => e.Empresa)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosComFiliais()
        {
            return await Db.Produtos.AsNoTracking()
                .Include(p => p.Nome )
                .OrderBy(p => p.Filial)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorEmpresa(Guid empresaId)
        {
            return await Buscar(e => e.EmpresaId == empresaId);
                
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFilial(Guid filialId)
        {
            return await Buscar(f => f.FilialId == filialId);
        }
    }
}
