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

        public async Task<IList<Emprestimo>> ObterEmprestimoPorAluno(Guid id)
        {
            return await Context.Emprestimos
                        .AsNoTracking()
                        .Include(x => x.Aluno)
                        .Where(x => x.AlunoId == id)
                        .ToListAsync();
        }


        public async Task<IList<Emprestimo>> ObterPendenteDeDevolucao()
        {
            return await Context.Emprestimos
                        .AsNoTracking()
                        .Include(x => x.Aluno)
                        .Where(x => x.Devolucao < DateTime.Now)
                        .ToListAsync();
        }
    }
}
