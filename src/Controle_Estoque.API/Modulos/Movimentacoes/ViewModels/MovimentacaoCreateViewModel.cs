using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Controle_Estoque.API.Modulos.Movimentacoes.ViewModels
{
    public class MovimentacaoCreateViewModel
    {

        // pra criar uma movimentacao

        [DefaultValue(null)]
        public Guid? EmpresaId { get; set; }

        [DefaultValue(null)] // Faz com que o Swagger exiba null por padrão
        public Guid? FilialId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid ProdutoId { get; set; } // Chave estrangeira para Produto

        public IMovimentacao TipoMovimentacao { get; set; } // Enum para garantir valores válidos

        [Required(ErrorMessage = "Valor tem que ser do tipo inteiro.")]
        public int Quantidade { get; set; }

    }
}
