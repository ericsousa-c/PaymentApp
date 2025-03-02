﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentApp.Infrastructure.Context;

#nullable disable

namespace PaymentApp.Infrastructure.Migrations
{
    [DbContext(typeof(PaymentAppDbContext))]
    partial class PaymentAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PaymentApp.Domain.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("email");

                    b.Property<Guid?>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.ToTable("cliente", (string)null);
                });

            modelBuilder.Entity("PaymentApp.Domain.Entities.Pagamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int")
                        .HasColumnName("cliente_id");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime")
                        .HasColumnName("data");

                    b.Property<Guid?>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("valor");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("pagamento", (string)null);
                });

            modelBuilder.Entity("PaymentApp.Domain.Entities.Pagamento", b =>
                {
                    b.HasOne("PaymentApp.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Pagamentos")
                        .HasForeignKey("ClienteId")
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("PaymentApp.Domain.Entities.Cliente", b =>
                {
                    b.Navigation("Pagamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
