using Controle_Estoque.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Infra.Mappings.Filiais
{
    public class FilialMapping : IEntityTypeConfiguration<Filial>
    {

        public void Configure(EntityTypeBuilder<Filial> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(f => f.Descricao)
                .HasMaxLength(500);

            builder.Property(f => f.CNPJ)
             .IsRequired()
             .HasMaxLength(18);

            builder.HasOne(f => f.Empresa) // Cada filial tem UMA empresa
           .WithMany(e => e.Filiais)  // Uma empresa pode ter VÁRIAS filiais
           .HasForeignKey(f => f.EmpresaId)
           .OnDelete(DeleteBehavior.Cascade);


            // Define o nome da tabela no banco de dados (opcional)
            builder.ToTable("Filiais");

        }
    }
}
