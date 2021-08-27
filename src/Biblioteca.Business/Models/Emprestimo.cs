using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Business.Models
{
    public class Emprestimo : Entity
    {
        public Guid AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public Guid LivroId { get; set; }
        public Livro Livro { get; set; }

        public DateTime Data { get; set; }
        public DateTime Devolucao { get; set; }
    }
}
