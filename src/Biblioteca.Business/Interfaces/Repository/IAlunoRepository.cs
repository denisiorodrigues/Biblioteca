using Biblioteca.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces.Repository
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task<Aluno> ObterAlunoEmprestimo(Guid id);

        Task<Aluno> ObterAlunoEndereco(Guid id);

        Task<Aluno> ObterAlunoEmprestimoEndereco(Guid id);
    }
}
