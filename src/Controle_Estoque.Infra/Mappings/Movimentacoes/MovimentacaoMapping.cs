using Controle_Estoque.Domain.Entidades.Movimentacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Infra.Mappings.Movimentacoes
{
    public class MovimentacaoMapping : IEntityTypeConfiguration<Movimentacao>
    {
        public void Configure(EntityTypeBuilder<Movimentacao> builder)
        {
            builder.HasKey(m => m.Id);

            // Relacionamento com Produto (Obrigatório)
            builder.HasOne(m => m.Produto)
                .WithMany() // Sem navegação inversa explícita
                .HasForeignKey(m => m.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict); // Impede a exclusão em cascata

            // Relacionamento com Empresa (Opcional)
            builder.HasOne(m => m.Empresa)
                .WithMany()
                .HasForeignKey(m => m.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Filial (Opcional)
            builder.HasOne(m => m.Filial)
                .WithMany()
                .HasForeignKey(m => m.FilialId)
                .OnDelete(DeleteBehavior.Restrict);

            // Enum armazenado como string no banco de dados
            builder.Property(m => m.TipoMovimentacao)
                .HasConversion<int>() // Armazena o enum, entrada = 1 saida = 0
                .HasColumnType("int)")
                .IsRequired();

            // Quantidade de produtos movimentados
            builder.Property(m => m.Quantidade)
                .IsRequired();

            // Data da movimentação
            builder.Property(m => m.DataMovimentacao)
                .IsRequired()
                .HasColumnType("datetime");


            // Definir a tabela no banco de dados (opcional)
            builder.ToTable("Movimentacoes");



        }
    }
}
