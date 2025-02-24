using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades
{
    public class Empresa : Entity
    {
        //public long Id { get; set; } = default!; herda o Id de Entity

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public string? CNPJ { get; set; }

        // Relação: Uma empresa pode ter muitas filiais
        public ICollection<Filial> Filiais { get; set; } = default!;


    }
}
