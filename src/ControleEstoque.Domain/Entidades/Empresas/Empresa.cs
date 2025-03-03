using Controle_Estoque.Domain.Entidades.Filiais;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades.Empresas
{
    public class Empresa : Entity
    {

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public string? CNPJ { get; set; }

        [NotMapped]
        public Guid FilialId { get; set; }

        // criar data de cadastro da empresa

        public ICollection<Filial>? Filiais { get; set; }


    }
}
