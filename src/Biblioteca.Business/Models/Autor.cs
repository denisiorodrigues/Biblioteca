using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Business.Models
{
    public class Autor : Entity
    {
        public string Nome { get; set; }

        public IEnumerable<Escrito> Livros { get;  set; }
    }
}
