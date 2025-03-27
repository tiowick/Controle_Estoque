using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Interfaces.Filiais;
using Controle_Estoque.Entidades.Validacoes.Documento.Padronizar.Texto;
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
        private readonly IFilialRepositorio _filialRepositorio;

        public FilialValidacao(IFilialRepositorio filialRepositorio)
        {
            _filialRepositorio = filialRepositorio;

            RuleFor(f => f.EmpresaId)
                .NotEmpty().WithMessage("A {PropertyName} deve estar associada a uma empresa.")
                .MustAsync(async (empresaId, cancellationToken) =>
                    await _filialRepositorio.ObterFiliaisPorEmpresa(empresaId) != null)
                .WithMessage("A empresa informada não existe.");

            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O nome da {PropertyName} é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da {PropertyName} deve ter no máximo 100 caracteres.");

            RuleFor(f => f.CNPJ)
                .NotEmpty().WithMessage("O CNPJ da {PropertyName} é obrigatório.")
                .Length(14).WithMessage("O CNPJ deve conter exatamente 14 dígitos.")
                .Matches(@"^\d{14}$").WithMessage("O CNPJ deve conter apenas números.")
                .MustAsync(async (filial, cnpj, cancellationToken) =>
                {
                    var filialExistente = await _filialRepositorio.ObterPorCNPJ(cnpj);
                    // Permitir se não existir ou se o registro encontrado for o mesmo que está sendo atualizado
                    return filialExistente == null || filialExistente.Id == filial.Id;
                })
                .WithMessage("Já existe uma filial com esse CNPJ informado.");

        }
    }

}
