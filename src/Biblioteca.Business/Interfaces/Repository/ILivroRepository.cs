using Biblioteca.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces.Repository
{
    public interface ILivroRepository : IRepository<Livro>
    {
        Task<IEnumerable<Livro>> ObterLivroPorAutor(Guid id);
        
        Task<Livro> ObterLivroAutorEmprestimo(Guid id);

        Task<bool> TemEmprestimo(Guid id);
        Task<IEnumerable<Livro>> ObterLivrosAutores(Guid id);
    }
}
