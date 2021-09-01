using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Models;
using Biblioteca.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly BibliotecaContext Context;
        protected readonly DbSet<TEntity> DbSet;
        
        public Repository(BibliotecaContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }
        public async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();

        }

        public async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity() { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
