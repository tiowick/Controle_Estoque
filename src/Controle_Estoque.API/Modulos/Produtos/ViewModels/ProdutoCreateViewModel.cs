using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.ComponentModel;
using Newtonsoft.Json;
using JsonConverter = Newtonsoft.Json.JsonConverter;
using Controle_Estoque.Infra.Padronizar.Texto;

namespace Controle_Estoque.API.Modulos.Produtos.ViewModels
{
    public class ProdutoCreateViewModel
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid EmpresaId { get; set; }

        [DefaultValue(null)] // Faz com que o Swagger exiba null por padrão
        public Guid? FilialId { get; set; }

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


