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
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(BibliotecaContext context) :
            base(context)
        {

        }

        public async Task<Aluno> ObterAlunoEmprestimo(Guid id)
        {
            return await Context
                        .Alunos
                        .Include(x => x.Emprestimos)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Aluno> ObterAlunoEndereco(Guid id)
        {
            return await Context
                         .Alunos
                         .Include(x => x.Endereco)
                         .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Aluno> ObterAlunoEmprestimoEndereco(Guid id)
        {
            return await Context
                         .Alunos
                         .Include(x => x.Endereco)
                         .Include(x => x.Emprestimos)
                         .ThenInclude(x => x.Livro)
                         .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
