using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Infrastructure.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> entity)
        {

            entity.HasKey(e => e.Id);

            entity.ToTable("cliente");

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.Nome)
                .HasColumnName("nome")
                .HasMaxLength(150);

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(150);

        }
    }
}

