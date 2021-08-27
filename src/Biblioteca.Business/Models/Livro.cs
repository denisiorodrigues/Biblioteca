using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Business.Models
{
    public class Livro : Entity
    {
        public string Titulo { get; set; }
        public string Resumo { get; set; }

        public int Exemplares { get; set; } 
        public DateTime Publicado { get; set; }
        public string Imagem { get; set; }
        public SituaoLivroEnum Situacao { get; set; }
        
        public IEnumerable<Escrito> Autores { get; set; }
        public IEnumerable<Emprestimo> Emprestimos { get; set; }
    }
}
