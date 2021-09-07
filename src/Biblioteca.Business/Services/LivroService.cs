using Biblioteca.Business.Interfaces;
using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Interfaces.Services;
using Biblioteca.Business.Models;
using System;
using System.Threading.Tasks;

namespace Biblioteca.Business.Services
{
    public class LivroService : BaseService, ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository, INotificador notificador)
            : base(notificador)
        {
            _livroRepository = livroRepository;
        }

        public async Task Adicionar(Livro livro)
        {
            await _livroRepository.Adicionar(livro);
        }

        public async Task Atualizar(Livro livro)
        {
            await _livroRepository.Atualizar(livro);
        }

        public async Task Remover(Guid id)
        {
            if (await _livroRepository.TemEmprestimo(id))
            {
                Notificar("O livro não pode ser excluído por contem emprestimo ativo.");
            }

            await _livroRepository.Remover(id);
        }

        public void Dispose()
        {
            _livroRepository?.Dispose();
        }
    }
}
