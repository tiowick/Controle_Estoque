using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades
{
    public class Produto : Entity
    {
        // o Id com guid ja herda de Entity 

        public Guid EmpresaId { get; set; } // Chave estrangeira para Empresa

        public Guid? FilialId { get; set; } // Chave estrangeira para Filial

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public DateTime? DataCadastro { get; set; }

        public decimal Preco { get; set; }

        public bool Ativo { get; set; }

        // Propriedades de navegação
        public Empresa? Empresa { get; set; }

        public Filial? Filial { get; set; }
    }
}
