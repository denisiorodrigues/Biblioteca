using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Models;
using Biblioteca.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data.Repository
{
    public class EscritoRepository : IEscritoRepository
    {
        protected readonly BibliotecaContext Context;
        protected readonly DbSet<Escrito> DbSet;

        public EscritoRepository(BibliotecaContext context)
        {
            Context = context;
            DbSet = Context.Set<Escrito>();
        }

        public async Task<IEnumerable<Escrito>> Buscar(Expression<Func<Escrito, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task Adicionar(Escrito escrito)
        {
            DbSet.Add(escrito);
            await SaveChanges();
        }

        public async Task Atualizar(Escrito escrito)
        {
            DbSet.Update(escrito);
            await SaveChanges();
        }

        public async Task Remover(Escrito escrito)
        {
            DbSet.Remove(escrito);
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