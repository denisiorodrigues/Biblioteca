using System;

namespace Biblioteca.API.DTO
{
    public class EscritoDTO
    {
        public Guid AutorId { get; set; }
        
        public AutorDTO Autor { get; set; }

        public Guid LivroId { get; set; }
        
        public LivroDTO Livro { get; set; }
    }
}