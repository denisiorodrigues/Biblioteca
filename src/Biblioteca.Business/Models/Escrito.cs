using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Business.Models
{
    public class Escrito
    {
        public Guid AutorId { get; set; }
        
        public Autor Autor { get; set; }

        public Guid LivroId { get; set; }
        
        public Livro Livro { get; set; }
    }
}
