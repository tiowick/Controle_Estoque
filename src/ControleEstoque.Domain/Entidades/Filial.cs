using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades
{
    public class Filial : Entity
    {
        //public long Id { get; set; } = default!; // Chave primária herda de Entity

        public Guid EmpresaId { get; set; } // Chave estrangeira para Empresa

        public string? Nome { get; set; } // Nome da filial

        public string? CNPJ { get; set; } // CNPj da filial

        // Propriedade de navegação
        public Empresa? Empresa { get; set; }
    }
}
