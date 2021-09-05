using Biblioteca.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces.Services
{
    public interface IAlunoService
    {
        Task Adicionar(Aluno aluno);
        Task Atualizar(Aluno aluno);
        Task Remover(Guid id);
        Task AtualizarEndereco(Endereco endereco);
        void Dispose();
    }
}
