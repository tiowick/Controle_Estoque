using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Controle_Estoque.API.Modulos.Filiais.ViewModels;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;

namespace Controle_Estoque.API.Modulos.Movimentacoes.ViewModels
{
    public class MovimentacaoViewModel
    {
        public Guid Id { get; set; }

        [DefaultValue(null)]
        public Guid? EmpresaId { get; set; }

        [DefaultValue(null)] // Faz com que o Swagger exiba null por padrão
        public Guid? FilialId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid ProdutoId { get; set; } // Chave estrangeira para Produto

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public TiposMovimentacoes TipoMovimentacao { get; set; } // Enum para garantir valores válidos

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int Quantidade { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        public DateTime DataMovimentacao { get; set; }

    }
}
