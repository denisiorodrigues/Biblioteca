using Biblioteca.Business.Models;
using System;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces.Services
{
    public interface IAutorService
    {
        Task Adicionar(Autor aluno);
        Task Atualizar(Autor aluno);
        Task Remover(Guid id);
        void Dispose();
    }
}
