using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Biblioteca.Business.Models;

namespace Biblioteca.Business.Interfaces.Repository
{
    public interface IEscritoRepository : IDisposable
    {
        Task Adicionar(Escrito escrito);
        Task Adicionar(IEnumerable<Escrito> escritos);
        Task Atualizar(Escrito escrito);
        Task Atualizar(IEnumerable<Escrito> escritos);
        Task Remover(Escrito escrito);
        void RemoverPorLivro(Guid id);
        Task<IEnumerable<Escrito>> Buscar(Expression<Func<Escrito, bool>> predicate);
        Task<int> SaveChanges();
    }
}