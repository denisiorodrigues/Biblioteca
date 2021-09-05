using Biblioteca.Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Business.Validations
{
    public class LivroValidation : AbstractValidator<Livro>
    {
        public LivroValidation()
        {
            RuleFor(f => f.Titulo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(3, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength}");

            RuleFor(f => f.Resumo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(3, 500).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength}");

            RuleFor(f => f.Exemplares)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Equal(0).WithMessage("O campo {PropertyName} presisa ser maior que ZERO.");

            RuleFor(f => f.Publicado)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");

            RuleFor(f => f.Imagem)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 255).WithMessage("O nome da imagem deve ter entre {MinLength} e {MaxLength}.");
            
            RuleFor(f => f.Situacao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");
        }
    }
}
