using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Business.Models
{
    public class Aluno : Entity
    {
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
        public IEnumerable<Emprestimo> Emprestimos { get; set; }
    }
}
