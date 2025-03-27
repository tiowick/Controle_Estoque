using Controle_Estoque.Domain.Entidades.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
                .HasColumnType("varchar(200)"); // nvarchar ocuparia o dobro de espaço

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("varchar(2000)");

            builder.Property(x => x.DataCadastro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")  // Para pegar a data atual no MySQL
                .ValueGeneratedOnAdd()         // Garante que o valor será gerado pelo banco
                .IsRequired()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); // Ignora em update;

            builder.Property(x => x.Preco)
                .HasColumnType("decimal(10,2)")
                .IsRequired();


            builder.ToTable("Produtos");
        }
    }
}
