using Controle_Estoque.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Infra.Mappings
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(e => e.Id);

            // Define o nome da tabela no banco de dados (opcional)
            builder.ToTable("Empresas");

            // Configuração das propriedades
            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Descricao)
                .HasMaxLength(500);

            builder.Property(e => e.CNPJ)
                .IsRequired()
                .HasMaxLength(18);

            // Relacionamento 1:N -> Uma empresa pode ter várias filiais
            builder.HasMany(e => e.Filiais)
                 .WithOne(f => f.Empresa)
                 .HasForeignKey(f => f.EmpresaId)
                 .OnDelete(DeleteBehavior.Cascade); // Se deletar a empresa, filiais são deletadas

            // Índice para melhorar a busca por CNPJ
            builder.HasIndex(e => e.CNPJ).IsUnique();
        }
    }
}
