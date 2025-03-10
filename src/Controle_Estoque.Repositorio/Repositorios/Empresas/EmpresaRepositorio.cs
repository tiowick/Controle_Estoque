﻿using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
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
            return await Db.Empresas.AsNoTracking()
                 .Include(f => f.Filiais)
                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Empresa>> ObterEmpresas()
        {
            return await Db.Empresas.AsNoTracking()
                .Include(e => e.Filiais)
                .OrderBy(e => e.Descricao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Empresa>> ObterEmpresasComFiliais()
        {
            return await Db.Empresas.AsNoTracking()
                .Include(f => f.Filiais) //propiedade de navegação
                .OrderBy(e => e.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Empresa>> ObterFiliaisPorEmpresa(Guid filialId)
        {
            return await Buscar(e => e.FilialId == filialId);
        }

        public async Task<Empresa> ObterPorCNPJ(string? cnpj)
        {
            return await Db.Empresas.FirstOrDefaultAsync(f => f.CNPJ == cnpj);
        }
    }
}
