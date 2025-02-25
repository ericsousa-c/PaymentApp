using PaymentApp.Domain.Enum;

namespace PaymentApp.Domain.Entities
{
    public partial class Pagamento
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        public enumStatusPagamento Status { get; set; }


        private Pagamento() { }

        public static Pagamento CriarPagamento(decimal valor, Cliente cliente)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do pagamento deve ser maior que zero.");
            }

            if (cliente == null)
            {
                throw new ArgumentException("É necessário um cliente válido para criar um pagamento");
            }


            return new Pagamento
            {
                Cliente = cliente,
                Valor = valor,
                Data = DateTime.UtcNow,
                Status = enumStatusPagamento.Pendente
            };
        }


        public void AtualizarStatusPagamento(enumStatusPagamento novoStatus)
        {
            if (Status == enumStatusPagamento.Pago)
            {
                throw new InvalidOperationException("Não é possível alterar um pagamento pago.");
            }

            if (Status == enumStatusPagamento.Cancelado)
            {
                throw new InvalidOperationException("Não é possível alterar um pagamento cancelado.");
            }

            Status = novoStatus;
        }
    }
}
