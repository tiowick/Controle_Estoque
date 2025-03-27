using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Entidades.Validacoes.Padronizar.Texto;
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

        public async Task<Produto?> ObterProdutoPorIdComEmpresa(Guid? id)
        {

            try
            {

                return await Db.Produtos.AsNoTracking()
                    .Include(e => e.Empresa)
                    .FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }
        }
            
        

        public async Task<Produto?> ObterProdutoPorIdComFilial(Guid? id)
        {
            try
            {
                return await Db.Produtos.AsNoTracking()
                .Include(e => e.Filial)
                .FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

            
        }

        public async Task<IEnumerable<Produto>> ObterProdutos()
        {
            try
            {
                return await Db.Produtos.AsNoTracking()
                .Include(p => p.Empresa)
                .OrderBy(e => e.Nome)
                .ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }


            
        }

        public async Task<IEnumerable<Produto>> ObterProdutosComFiliais()
        {

            try
            {
                return await Db.Produtos.AsNoTracking()
               .Include(p => p.Nome)
               .OrderBy(p => p.Filial)
               .ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

           
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorEmpresa(Guid empresaId)
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

        public async Task<IEnumerable<Produto>> ObterProdutosPorFilial(Guid filialId)
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
    }
}
