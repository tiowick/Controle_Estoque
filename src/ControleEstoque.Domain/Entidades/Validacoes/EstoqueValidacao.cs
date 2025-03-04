using Controle_Estoque.Domain.Entidades.Estoques;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Enuns;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades.Validacoes
{
    public class EstoqueValidacao : AbstractValidator<Estoque>
    {
        public EstoqueValidacao()
        {
            // ProdutoId é obrigatório e não pode ser Guid.Empty
            RuleFor(e => e.ProdutoId)
                .NotEmpty().WithMessage("O ProdutoId é obrigatório.");

            // EmpresaId é obrigatório e não pode ser Guid.Empty
            RuleFor(e => e.EmpresaId)
                .NotEmpty().WithMessage("O EmpresaId é obrigatório.");

            // A quantidade deve ser maior ou igual a zero
            RuleFor(e => e.Quantidade)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade deve ser maior ou igual a zero.");

            // Se o tipo de estoque for 'filial', então o FilialId deve ser informado
            RuleFor(e => e)
                .Must(e => e.TipoIdentificador == ITipoIdentificadorProduto.empresa ||
                  (e.TipoIdentificador == ITipoIdentificadorProduto.filial && 
                   e.FilialId.HasValue && e.FilialId != Guid.Empty))
                .WithMessage("Para estoque de filial, o FilialId é obrigatório.");

            // Se o tipo de estoque for 'empresa', o FilialId deve ser nulo
            RuleFor(e => e)
                .Must(e => e.TipoIdentificador != ITipoIdentificadorProduto.empresa || e.FilialId == null)
                .WithMessage("Para estoque da empresa, o FilialId deve ser nulo.");
        }
    }
}
