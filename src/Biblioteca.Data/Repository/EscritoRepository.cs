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

        //Não foi colocado como assincrono de propósito
        public void RemoverPorLivro(Guid id)
        {
            DbSet.RemoveRange(DbSet.Where(x => x.LivroId == id));
        }

        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public async Task Adicionar(IEnumerable<Escrito> escritos)
        {
            //TODO: Tentar mudar para BulkInsert
            foreach (var item in escritos)
            {
                await Adicionar(item);
            }
        }

        public async Task Atualizar(IEnumerable<Escrito> escritos)
        {
            //TODO: Tentar mudar para BulkInsert
            foreach (var item in escritos)
            {
                await Atualizar(item);
            }
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}