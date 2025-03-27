using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Entidades.Validacoes.Padronizar.Texto;
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

        public async Task<Empresa> ObterEmpresaPorIdComFiliais(Guid id)
        {
            try
            {
                return await Db.Empresas.AsNoTracking()
                 .Include(f => f.Filiais)
                 .FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }
            
        }

        public async Task<IEnumerable<Empresa>> ObterEmpresas()
        {

            try
            {
                return await Db.Empresas.AsNoTracking()
                .Include(e => e.Filiais)
                .OrderBy(e => e.Descricao)
                .ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }
            
        }

        public async Task<IEnumerable<Empresa>> ObterEmpresasComFiliais()
        {
            try
            {
                return await Db.Empresas.AsNoTracking()
                .Include(f => f.Filiais) //propiedade de navegação
                .OrderBy(e => e.Nome)
                .ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

            
        }

        public async Task<IEnumerable<Empresa>> ObterFiliaisPorEmpresa(Guid filialId)
        {
            try
            {
                return await Buscar(e => e.FilialId == filialId);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

           
        }

        public async Task<Empresa> ObterPorCNPJ(string? cnpj)
        {
            try
            {
                return await Db.Empresas.FirstOrDefaultAsync(f => f.CNPJ == cnpj);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }
            
        }
    }
}
