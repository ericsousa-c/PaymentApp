using PaymentApp.Application.DTOs;
using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Enum;

namespace PaymentApp.Application.Interfaces.Services
{
    public interface IPagamentoService
    {
        Task<Pagamento> CriarPagamento(decimal valor, Cliente cliente);
        Task<bool> AtualizarStatusPagamento(int pagamentoId, string novoStatus);
        Task<PagamentoDto> CriarPagamento(CriarPagamentoDto pagamentoDto);
        Task<IEnumerable<PagamentoDto>> ObterPagamentosPorCliente(int clienteId);

    }

}


