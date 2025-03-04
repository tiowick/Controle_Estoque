using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades.Estoques
{
    public class Estoque : Entity
    {

        // Chave do produto para o qual esse registro controla o estoque
        public Guid ProdutoId { get; set; }

        // Empresa à qual pertence o estoque (sempre preenchido)
        public Guid EmpresaId { get; set; }

        // Se o estoque for específico de uma filial, esse campo será preenchido; caso contrário, nulo
        public Guid? FilialId { get; set; }

        // Quantidade disponível no estoque
        public int Quantidade { get; set; }

        // Data da última atualização do estoque
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;

        // Indica se o estoque é da empresa ou de uma filial
        public ITipoIdentificadorProduto TipoIdentificador { get; set; }

        // Propriedades de navegação
        public Produto Produto { get; set; } = null!;
        public Empresa Empresa { get; set; } = null!;
        public Filial? Filial { get; set; }
    }
}
