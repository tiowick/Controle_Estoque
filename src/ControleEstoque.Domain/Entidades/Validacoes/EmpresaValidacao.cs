using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Interfaces.Empresas;
using Controle_Estoque.Domain.Interfaces.Filiais;
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
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public EmpresaValidacao(IEmpresaRepositorio empresaRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;


            RuleFor(e => e.Nome)
            .NotEmpty().WithMessage("O nome da {PropertyName} é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da {PropertyName}, deve ter no máximo 100 caracteres.");

            RuleFor(e => e.Descricao)
                .MaximumLength(255).WithMessage("A {PropertyName} deve ter no máximo 255 caracteres.");

            RuleFor(f => f.CNPJ)
               .NotEmpty().WithMessage("O CNPJ da {PropertyName} é obrigatório.")
               .Length(14).WithMessage("O CNPJ deve conter exatamente 14 dígitos.")
               .Matches(@"^\d{14}$").WithMessage("O CNPJ deve conter apenas números.")
               .MustAsync(async (empresa, cnpj, cancellationToken) =>
               {
                   var _empresa = await _empresaRepositorio.ObterPorCNPJ(cnpj);
                   // Permitir se não existir ou se o registro encontrado for o mesmo que está sendo atualizado
                   return _empresa == null || _empresa.Id == empresa.Id;
               })
               .WithMessage("Já existe uma filial com esse CNPJ informado.");
        }
    }
}
