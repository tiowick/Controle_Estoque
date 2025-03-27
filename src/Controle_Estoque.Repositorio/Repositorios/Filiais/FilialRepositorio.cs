using Controle_Estoque.Domain.Entidades;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Entidades.Validacoes.Padronizar.Texto;
using Controle_Estoque.Domain.Enuns;
using Controle_Estoque.Domain.Interfaces.Filiais;
using Controle_Estoque.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<IEnumerable<Filial>> ObterFiliaisComEmpresa()
        {

            try
            {
                return await Db.Filiais.AsNoTracking()
                   .Include(f => f.Empresa)
                   .OrderBy(f => f.Nome)
                   .ToListAsync()
                   .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

        }



        public async Task<IEnumerable<Filial>> ObterFiliaisPorEmpresa(Guid empresaId)
        {

            try
            {
                var _return = await Buscar(e => e.EmpresaId == empresaId);

                return await Task.FromResult(_return).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

        }

        public async Task<IEnumerable<Filial>> Obterfiliais()
        {
            try
            {
                return await Db.Filiais.AsNoTracking()
                 .Include(f => f.Empresa)
                 .OrderBy(f => f.Nome)
                 .ToListAsync()
                 .ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }
        }



        public async Task<Filial> ObterPorCNPJ(string? cnpj)
        {
            try
            {
                var _return = await Db.Filiais.FirstOrDefaultAsync(f => f.CNPJ == cnpj);

                return await Task.FromResult(_return).ConfigureAwait(false);
            }

            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }


        }

    }
}
