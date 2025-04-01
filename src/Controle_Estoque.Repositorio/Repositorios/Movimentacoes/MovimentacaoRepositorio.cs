using Controle_Estoque.Domain.Entidades.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Entidades.Validacoes.Padronizar.Texto;
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
            try
            {
                return await Db.Movimentacoes.AsNoTracking()
                .Include(e => e.Produto)
                .OrderBy(p => p.Quantidade)
                .ToListAsync().ConfigureAwait(false);
            
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

            
        }


        public async Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorEmpresa(Guid empresaId)
        {
            try
            {
                return await Buscar(e => e.EmpresaId == empresaId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

        }

        public async Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorFilial(Guid filialId)
        {
            try
            {

                return await Buscar(f => f.FilialId == filialId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }
        }

        public async Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorProduto(Guid produtoId)
        {

            try
            {
                return await Buscar(p => p.ProdutoId == produtoId);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

           
        }

        //public async Task<IEnumerable<Movimentacao>> ObterMovimentacoesPorTipo(TiposMovimentacoes tipo) // entrada e saida
        //{
        //    try
        //    {
        //        return await Buscar(m => m.TipoMovimentacao == tipo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new TratamentoExcecao
        //            (ex.Message.Traduzir());
        //    }
            
        //}
    }
}
