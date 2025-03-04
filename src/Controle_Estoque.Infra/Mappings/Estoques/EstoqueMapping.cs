using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controle_Estoque.Domain.Entidades.Estoques;

namespace Controle_Estoque.Infra.Mappings.Estoques
{
    public class EstoqueMapping : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantidade)
                .IsRequired();

            builder.Property(x => x.DataAtualizacao)
                .IsRequired();

            // Mapeia o enum como inteiro
            builder.Property(x => x.TipoIdentificador)
                .IsRequired()
                .HasConversion<int>();

            // Relacionamento com Produto
            builder.HasOne(x => x.Produto)
                .WithMany()
                .HasForeignKey(x => x.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento com Empresa
            builder.HasOne(x => x.Empresa)
                .WithMany()
                .HasForeignKey(x => x.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento opcional com Filial
            builder.HasOne(x => x.Filial)
                .WithMany()
                .HasForeignKey(x => x.FilialId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Estoques");
        }
    }
}
