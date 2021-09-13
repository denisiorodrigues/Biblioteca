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
        Task Atualizar(Escrito escrito);
        Task Remover(Escrito escrito);
        Task<IEnumerable<Escrito>> Buscar(Expression<Func<Escrito, bool>> predicate);
        Task<int> SaveChanges();
    }
}