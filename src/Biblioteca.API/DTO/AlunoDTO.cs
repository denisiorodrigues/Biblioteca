using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.API.DTO
{
    public class AlunoDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required( ErrorMessage ="O campo {0} é orbigatório")]
        [StringLength(200, ErrorMessage ="O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength =2)]
        public string Nome { get; set; }

        public EnderecoDTO Endereco { get; set; }
        public IEnumerable<EmprestimoDTO> Emprestimos { get; set; }
    }
}
