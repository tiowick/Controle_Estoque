using Controle_Estoque.Domain.Entidades.Empresas;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades.Validacoes
{
    public class EmpresaValidacao : AbstractValidator<Empresa>
    {
        public EmpresaValidacao()
        {
            RuleFor(e => e.Nome)
            .NotEmpty().WithMessage("O nome da {PropertyName} é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da {PropertyName}, deve ter no máximo 100 caracteres.");

            RuleFor(e => e.Descricao)
                .MaximumLength(255).WithMessage("A {PropertyName} deve ter no máximo 255 caracteres.");

            // possivel datacadastro

            RuleFor(e => e.CNPJ)
                .NotEmpty().WithMessage("O {PropertyName} é obrigatório.")
                .Length(14).WithMessage("O {PropertyName} deve conter exatamente 14 dígitos.")
                .Matches(@"^\d{14}$").WithMessage("O {PropertyName} deve conter apenas números.");
        }
    }
}
