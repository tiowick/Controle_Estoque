using System.ComponentModel.DataAnnotations;

namespace Controle_Estoque.API.Modulos.Filiais.ViewModels
{
    public class FilialUpdateViewModel
    {
        //public Guid Id { get; set; }

        public Guid EmpresaId { get; set; } // Chave estrangeira para Empresa

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Nome { get; set; } // Nome da filial

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Descricao { get; set; } // Descricao da filial

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string? CNPJ { get; set; } // CNPj da filial
    }
}
