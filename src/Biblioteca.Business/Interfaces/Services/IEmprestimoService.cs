using Biblioteca.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces.Services
{
    public interface IEmprestimoService
    {
        Task Emprestar(Emprestimo emprestimo);
        Task Devolver(Emprestimo esmprestimo);
        Task Atualizar(Emprestimo emprestimo);
        void Dispose();
    }
}
