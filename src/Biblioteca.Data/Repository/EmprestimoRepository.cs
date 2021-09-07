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
    public class EmprestimoRepository : Repository<Emprestimo>, IEmprestimoRepository
    {
        public EmprestimoRepository(BibliotecaContext context)
            :base(context)
        {

        }

        public async Task<IList<Emprestimo>> ObterPendenteDeDevolucao()
        {
            return await Context.Emprestimos
                        .AsNoTracking()
                        .Include(x => x.Aluno)
                        .Include(x => x.Livro)
                        .Where(x => x.Devolucao < DateTime.Now)
                        .ToListAsync();
        }

        public async Task<IList<Emprestimo>> ObterEmprestimoPorAluno(Guid id)
        {
            return await Context.Emprestimos
                        .AsNoTracking()
                        .Include(x => x.Aluno)
                        .Include(x => x.Livro)
                        .Where(x => x.AlunoId == id)
                        .ToListAsync();
        }

        public async Task<IList<Emprestimo>> ObterEmprestimoPorLivro(Guid id)
        {
            return await Context.Emprestimos
                        .AsNoTracking()
                        .Include(x => x.Aluno)
                        .Include(x => x.Livro)
                        .Where(x => x.LivroId == id)
                        .ToListAsync();
        }

        public async Task<IList<Emprestimo>> ObterEmprestimoPedentePorAluno(Guid id)
        {
            return await Context.Emprestimos
                        .AsNoTracking()
                        .Include(x => x.Aluno)
                        .Include(x => x.Livro)
                        .Where(x => x.AlunoId == id 
                            && x.Devolucao < DateTime.Now
                            && x.Devolvido == null)
                        .ToListAsync();
        }

        public async Task<IList<Emprestimo>> ObterEmprestimoPedentePorLivro(Guid id)
        {
            return await Context.Emprestimos
                        .AsNoTracking()
                        .Include(x => x.Aluno)
                        .Include(x => x.Livro)
                        .Where(x => x.LivroId == id 
                                && x.Devolucao < DateTime.Now
                                && x.Devolvido == null)
                        .ToListAsync();
        }
    }
}
