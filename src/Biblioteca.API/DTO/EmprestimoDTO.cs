using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.API.DTO
{
    public class EmprestimoDTO
    {

        [Required(ErrorMessage = "O campo {0} é orbigatório")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O campo {0} é orbigatório")]
        public DateTime Devolucao { get; set; }
        
        public AlunoDTO Aluno { get; set; }
        
        public LivroDTO Livro { get; set; }

        public DateTime? Devolvido { get; set; }

        public Guid LivroId { get; set; }

        public Guid AlunoId { get; set; }
    }
}
