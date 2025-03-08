using Controle_Estoque.Domain.Entidades.Movimentacoes;
using Controle_Estoque.Domain.Enuns;
using Controle_Estoque.Domain.Interfaces.Movimentacoes;
using Controle_Estoque.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Repositorio.Repositorios.Movimentacoes
{
    public class MovimentacaoRepositorio : Repositorio<Movimentacao>, IMovimentacaoRepositorio
    {
        
        public MovimentacaoRepositorio(AppDbContext context) : base(context)
        {

        }

        public async Task<Movimentacao> ObterMovimentacaoPorId(Guid id)
        {
            return await ObterPorId(id);
        }


        public async Task<IEnumerable<Movimentacao>> ObterTodasMovimentacoes()
        {
            return await Db.Movimentacoes.AsNoTracking()
                .Include(e => e.Produto)
                .OrderBy(p => p.Quantidade)
                .ToListAsync();
        }


        public async Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorEmpresa(Guid empresaId)
        {
            return await Buscar(e => e.EmpresaId == empresaId);
        }

        public async Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorFilial(Guid filialId)
        {
            return await Buscar(f => f.FilialId == filialId);
        }

        public async Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorProduto(Guid produtoId)
        {
            return await Buscar(p => p.ProdutoId == produtoId);
        }

        public async Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorTipo(IMovimentacao tipo) // entrada e saida
        {
            return await Buscar(m => m.TipoMovimentacao == tipo);
        }
    }
}
