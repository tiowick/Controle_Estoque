using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades.Movimentacoes
{
    public class Movimentacao : Entity
    {
        public Guid ProdutoId { get; set; } // Chave estrangeira para Produto
        public Produto? Produto { get; set; }

        public Guid? EmpresaId { get; set; } // FK opcional para Empresa
        public Empresa? Empresa { get; set; }

        public Guid? FilialId { get; set; } // FK opcional para Filial
        public Filial? Filial { get; set; }

        public TiposMovimentacoes TipoMovimentacao { get; set; } // Enum para garantir valores válidos

        public int Quantidade { get; set; }

        public DateTime DataMovimentacao { get; set; } = DateTime.Now;
    }
}
