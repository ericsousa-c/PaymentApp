namespace PaymentApp.Application.DTOs
{
    public class CriarPagamentoDto
    {
        public int ClienteId { get; set; }
        public decimal Valor { get; set; }
    }

    public class AtualizarStatusPagamentoDto
    {
        public int PagamentoId { get; set; }
        public string NovoStatus { get; set; }
    }

    public class PagamentoDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
    }
}
