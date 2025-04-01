using System.ComponentModel.DataAnnotations;

namespace Controle_Estoque.API.Modulos.Estoques.ViewModels
{
    public class EstoqueUpdateViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Quantidade { get; set; }

        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
