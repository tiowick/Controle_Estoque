using System.ComponentModel.DataAnnotations;

namespace Controle_Estoque.API.Modulos.Produtos.ViewModels
{
    public class ProdutoUpdateViewModel
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid EmpresaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Preco { get; set; }

        public bool Ativo { get; set; }
    }
}
