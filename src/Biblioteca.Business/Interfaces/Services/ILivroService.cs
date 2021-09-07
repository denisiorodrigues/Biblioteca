using Biblioteca.Business.Models;
using System;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces.Services
{
    public interface ILivroService
    {
        Task Adicionar(Livro livro);
        Task Atualizar(Livro livro);
        Task Remover(Guid id);
        void Dispose();
    }
}
