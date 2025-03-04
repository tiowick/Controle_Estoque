using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Controle_Estoque.API.Modulos.Produtos.ViewModels
{
    public class ProdutoCreateViewModel
    {
       

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Descricao { get; set; }

        public DateTime? DataCadastro { get; set; } = DateTime.Now; // pegando a data atual

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Preco { get; set; }

        public bool Ativo { get; set; }


        //public Guid Id { get; set; }

        //public Guid EmpresaId { get; set; } // Chave estrangeira para Empresa

        //public Guid? FilialId { get; set; } // Chave estrangeira para Filial

        // //Propriedades de navegação
        //public Empresa? Empresa { get; set; }

        //public Filial? Filial { get; set; }
    }
}
