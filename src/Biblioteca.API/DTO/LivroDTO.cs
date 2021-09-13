using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.API.DTO
{
    public class LivroDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é orbigatório")]
        [StringLength(300, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é orbigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Resumo { get; set; }

        [Required(ErrorMessage = "O campo {0} é orbigatório")]
        public int Exemplares { get; set; }

        [Required(ErrorMessage = "O campo {0} é orbigatório")]
        public DateTime Publicado { get; set; }

        public string ImagemUpload { get; set; }

        [Required(ErrorMessage = "O campo {0} é orbigatório")]
        public int Situacao { get; set; }

        public IEnumerable<EscritoDTO> Autores { get; set; }

        public IEnumerable<EmprestimoDTO> Emprestimos { get; set; }
    }
}
