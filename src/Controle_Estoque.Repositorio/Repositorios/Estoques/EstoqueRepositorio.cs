using Controle_Estoque.Domain.Entidades.Estoques;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Entidades.Validacoes.Padronizar.Texto;
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

            try
            {
                return await Db.Estoques.AsNoTracking()
                    .Include(p => p.Produto)
                    .OrderBy(e => e.Quantidade)
                    .ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }


            
        }

        public async Task<IEnumerable<Estoque>> ObterEstoquePorEmpresaId(Guid empresaId)
        {
            try
            {
                return await Buscar(e => e.EmpresaId == empresaId)
                .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

            
        }

        public async Task<IEnumerable<Estoque>> ObterEstoquePorFilialId(Guid filialId)
        {
            try
            {
                return await Buscar(e => e.FilialId == filialId)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

           
        }

        public async Task<Estoque?> ObterEstoquePorProdutoId(Guid? produtoId)
        {

            try
            {
                return await Db.Estoques.AsNoTracking()
                  .Include(e => e.Produto)
                  .FirstOrDefaultAsync(e => e.Id == produtoId)
                  .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }
           
        }
    }
}
