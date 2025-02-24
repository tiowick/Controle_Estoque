using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades.Validacoes
{
    public class FilialValidacao : AbstractValidator<Filial>
    {
        public FilialValidacao()
        {
            RuleFor(f => f.EmpresaId)
            .NotEmpty().WithMessage("A {PropertyName} deve estar associada a uma empresa.");

            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O nome da {PropertyName} é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da {PropertyName} deve ter no máximo 100 caracteres.");

            RuleFor(f => f.CNPJ)
                .NotEmpty().WithMessage("O CNPJ da {PropertyName} é obrigatório.")
                .Length(14).WithMessage("O CNPJ deve conter exatamente 14 dígitos.")
                .Matches(@"^\d{14}$").WithMessage("O CNPJ deve conter apenas números.");
        }
    }
}
