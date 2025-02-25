using Microsoft.EntityFrameworkCore;
using PaymentApp.Domain.Entities;


namespace PaymentApp.Infrastructure.Context
{
    public class PaymentAppDbContext : DbContext
    {


        public PaymentAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Pagamento> Pagamentos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //registrando via reflection todos os configurations das entidades do dbcontext
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentAppDbContext).Assembly);

            //desabilitar o cascade delete para todas as entidades
            foreach (var relationships in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationships.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);

        }


    }


}
