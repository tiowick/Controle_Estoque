using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Controle_Estoque.API.Modulos.Produtos.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }

        public Guid EmpresaId { get; set; }

        [DefaultValue(null)] // Faz com que o Swagger exiba null por padrão
        public Guid? FilialId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Descricao { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime DataCadastro { get; set; } 

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Preco { get; set; }

        public bool Ativo { get; set; }

        [JsonIgnore] // Para não serializar caso esteja usando JSON
        [NotMapped]
        public EmpresaViewModel? Empresa { get; set; }





    }
}
