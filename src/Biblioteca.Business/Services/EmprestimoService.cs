using Biblioteca.Business.Interfaces;
using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Interfaces.Services;
using Biblioteca.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Business.Services
{
    public class EmprestimoService : BaseService, IEmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;

        public EmprestimoService(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository, INotificador notificador)
            : base(notificador)
        {
            _emprestimoRepository = emprestimoRepository;
            _livroRepository = livroRepository;
        }

        public async Task Emprestar(Emprestimo emprestimo)
        {
            var emprestimos = await _emprestimoRepository.ObterEmprestimoPorAluno(emprestimo.AlunoId);
            if (emprestimos.Count() > 3)
            {
                Notificar($"Foi atingido o limite máximo de 3 emprestimos por aluno.");
            }

            var emprestimosLivros = await _emprestimoRepository.ObterEmprestimoPedentePorLivro(emprestimo.LivroId);

            if (emprestimosLivros.Count() > 0)
            {
                var livro = emprestimosLivros.FirstOrDefault().Livro;

                if (emprestimosLivros.Count() >= livro.Exemplares)
                {
                    Notificar($"Todos os {livro.Exemplares} exemplares do livro {livro.Titulo.ToUpper()} foram emprestados.");
                }
            }

            await _emprestimoRepository.Adicionar(emprestimo);
        }

        public async Task Devolver(Emprestimo emprestimo)
        {
            emprestimo.Devolvido = DateTime.Now;
            await Atualizar(emprestimo);
        }

        public async Task Atualizar(Emprestimo emprestimo)
        {
            await _emprestimoRepository.Atualizar(emprestimo);
        }

        public void Dispose()
        {
            _emprestimoRepository.Dispose();
        }
    }
}
