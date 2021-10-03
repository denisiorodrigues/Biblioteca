using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Models;
using Biblioteca.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data.Repository
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        public LivroRepository(BibliotecaContext context)
            :base(context)
        {
        }

        public async Task<bool> TemEmprestimo(Guid id)
        {
            return await Context.Livros
                .AsNoTracking()
                .Include(x => x.Emprestimos)
                .AnyAsync(x => x.Id == id && x.Emprestimos.Count() > 1);
        }

        public async Task<IEnumerable<Livro>> ObterLivroPorAutor(Guid id)
        {
            return await (from livro in Context.Livros
                          join escrito in Context.Escritos on livro.Id equals escrito.LivroId
                          join autor in Context.Autores on escrito.AutorId equals autor.Id
                          where autor.Id == id
                          select livro).ToListAsync();
        }

        public async Task<IEnumerable<Livro>> ObterLivrosAutores(Guid id)
        {
            return await Context.Livros
                        .AsNoTracking()
                        .Include(x => x.Emprestimos)
                        .Include(x => x.Autores)
                        .ThenInclude(x => x.Autor)
                        .Where(x => x.Id == id)
                        .ToListAsync();
        }

        public async Task<Livro> ObterLivroAutorEmprestimo(Guid id)
        {
            return await Context.Livros
                .AsNoTracking()
                .Include(x => x.Autores)
                .Include(x => x.Emprestimos)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
