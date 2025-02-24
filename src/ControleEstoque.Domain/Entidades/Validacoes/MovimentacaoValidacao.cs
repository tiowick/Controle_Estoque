using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades.Validacoes
{
    public class MovimentacaoValidacao : AbstractValidator<Movimentacao>
    {
        public MovimentacaoValidacao()
        {
            RuleFor(m => m.ProdutoId)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.");

            RuleFor(m => m.TipoMovimentacao)
                .IsInEnum().WithMessage("O tipo de {PropertyName} deve ser válido.");

            RuleFor(m => m.Quantidade)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

            RuleFor(m => m.DataMovimentacao)
                .NotEmpty().WithMessage("A data da {PropertyName} é obrigatória.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data da movimentação não pode ser no futuro.");

            RuleFor(m => m)
                .Must(m => m.EmpresaId.HasValue || m.FilialId.HasValue)
                .WithMessage("A movimentação  deve estar associada a uma empresa ou filial.");
        }
    }
}
