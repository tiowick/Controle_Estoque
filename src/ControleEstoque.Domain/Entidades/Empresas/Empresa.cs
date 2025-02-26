using Controle_Estoque.Domain.Entidades.Filiais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades.Empresas
{
    public class Empresa : Entity
    {
        //public long Id { get; set; } = default!; herda o Id de Entity

        public Guid FilialId { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public string? CNPJ { get; set; }

        public ICollection<Filial> Filiais { get; set; } = new List<Filial>();


    }
}
