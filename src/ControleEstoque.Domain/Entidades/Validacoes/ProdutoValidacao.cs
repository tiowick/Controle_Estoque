﻿using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Interfaces.Produtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades.Validacoes
{
    public class ProdutoValidacao : AbstractValidator<Produto>
    {

        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoValidacao(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;

            // Validação do Nome
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .Length(2, 100).WithMessage("O nome do produto deve ter entre 2 e 100 caracteres.");

            // Validação do Preço
            RuleFor(p => p.Preco)
                .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");

            // Validação da DataCadastro (não pode ser no futuro)
            RuleFor(p => p.DataCadastro)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de cadastro não pode ser no futuro.");

            // Validação da EmpresaId
            RuleFor(p => p.EmpresaId)
                .NotEmpty().WithMessage("A empresa é obrigatória.")
                .MustAsync(async (Empresa, id, cancellationToken) =>
                {
                     var _empresaExiste = await _produtoRepositorio.ObterPorId(id);
                     // Permitir se não existir ou se o registro encontrado for o mesmo que está sendo atualizado
                     return _empresaExiste == null || _empresaExiste.Id == Empresa.Id;
                })
                .WithMessage("Já existe uma filial com esse CNPJ informado.");


            // Validação do FilialId (se fornecido, precisa ser válido)
            RuleFor(p => p.FilialId)
                .Must((produto, filialId) => filialId == null || produto.EmpresaId != Guid.Empty)
                .WithMessage("Se o produto pertence a uma filial, a empresa precisa ser válida.");


           
        }

    }
    
}
