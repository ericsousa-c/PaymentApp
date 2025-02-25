using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Infrastructure.Mappings
{
    public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> entity)
        {

            entity.HasKey(e => e.Id);

            entity.ToTable("pagamento");

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.ClienteId)
                .HasColumnName("cliente_id");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.Valor)
                .HasColumnName("valor")
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.Data)
                .HasColumnName("data")
                .HasColumnType("datetime");
        }
    }
}

