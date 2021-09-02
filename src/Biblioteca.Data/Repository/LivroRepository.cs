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

        public async Task<IList<Livro>> ObterLivroPorAutor(Guid id)
        {
            return await (from livro in Context.Livros
                          join escrito in Context.Escritos on livro.Id equals escrito.LivroId
                          join autor in Context.Autores on escrito.AutorId equals autor.Id
                          where autor.Id == id
                          select livro).ToListAsync();
        }
    }
}
