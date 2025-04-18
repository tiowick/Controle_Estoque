﻿using Controle_Estoque.Domain.Entidades;
using Controle_Estoque.Domain.Entidades.Reflection;
using Controle_Estoque.Domain.Entidades.Validacoes.Padronizar.Texto;
using Controle_Estoque.Domain.Interfaces;
using Controle_Estoque.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Controle_Estoque.Repositorio.Repositorios
{
    public abstract class Repositorio<TEntity>
        : IRepositorio<TEntity>
        where TEntity : Entity, new()

    {

        protected readonly AppDbContext Db;
        protected readonly DbSet<TEntity> DbSet;


        protected Repositorio(AppDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            try
            {
                return await DbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

           
        }


        public virtual async Task<List<TEntity>> ObterTodos()
        {
            try
            {
                return await DbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

           
        }


        public virtual async Task Adicionar(TEntity entity)
        {
            try
            {

                DbSet.Add(entity);
                await SaveChanges();
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }


        }

        public virtual async Task Atualizar(TEntity entity)
        {
            try
            {
                DbSet.Update(entity);
                await SaveChanges();
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }            
        }


        public virtual async Task Remover(Guid id)
        {
            try
            {
                DbSet.Remove(new TEntity { Id = id }); //criando a variavel da instancia
                await SaveChanges();
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }

        }


        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            try
            {
                Db.Dispose();
            }
            catch (Exception ex)
            {
                throw new TratamentoExcecao
                    (ex.Message.Traduzir());
            }
            
        }
    }
}
