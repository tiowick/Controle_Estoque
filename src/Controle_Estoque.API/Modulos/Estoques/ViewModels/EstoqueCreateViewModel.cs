﻿using Controle_Estoque.Domain.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Controle_Estoque.API.Modulos.Estoques.ViewModels
{
    public class EstoqueCreateViewModel
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid ProdutoId { get; set; }

        // Empresa à qual pertence o estoque (sempre preenchido)
        [DefaultValue(null)]
        public Guid EmpresaId { get; set; }

        // Se o estoque for específico de uma filial, esse campo será preenchido; caso contrário, nulo
        [DefaultValue(null)]
        public Guid? FilialId { get; set; }

        public TipoIdentificadorProduto TipoIdentificador { get; set; }
        // Quantidade disponível no estoque
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Quantidade { get; set; }

        // Data da última atualização do estoque
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;

     
    }
}
