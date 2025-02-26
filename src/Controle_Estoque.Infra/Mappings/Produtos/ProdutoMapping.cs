using Controle_Estoque.Domain.Entidades.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Infra.Mappings.Produtos
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)"); // nvarchar ocupa o dobro de espaço

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("varchar(2000)");

            builder.ToTable("Produtos");
        }
    }
}
