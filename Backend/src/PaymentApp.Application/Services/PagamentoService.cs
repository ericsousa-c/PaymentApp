using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentApp.Application.DTOs;
using PaymentApp.Application.Interfaces.Services;
using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Enum;
using PaymentApp.Infrastructure.Context;

namespace PaymentApp.Application.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly PaymentAppDbContext _context;
        private readonly ILogger<PagamentoService> _logger;
        private readonly IClienteService _clienteService;

        public PagamentoService(PaymentAppDbContext context, 
                                ILogger<PagamentoService> logger,
                                IClienteService clienteService)
        {
            _context = context;
            _logger = logger;
            _clienteService = clienteService;
        }

        public async Task<bool> AtualizarStatusPagamento(int pagamentoId, string novoStatus)
        {
            try
            {
                var editRecord = await _context.Pagamentos.FindAsync(pagamentoId);

                if (editRecord == null)
                {
                    return false;
                }

                if (Enum.TryParse(novoStatus, out enumStatusPagamento status))
                {
                    editRecord.AtualizarStatusPagamento(status);
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar nome pagamento de {Id}", pagamentoId);
                return false;
            }
        }

        public async Task<Pagamento> CriarPagamento(decimal valor, Cliente cliente)
        {
            try {
                var newRecord = Pagamento.CriarPagamento(valor, cliente);
                _context.Pagamentos.Add(newRecord);
                await _context.SaveChangesAsync();

                return newRecord;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Erro ao criar o pagamento");
                return null;
            }


        }

        public async Task<PagamentoDto> CriarPagamento(CriarPagamentoDto pagamentoDto)
        {
            var clienteDb = await _context.Clientes.FindAsync(pagamentoDto.ClienteId);

            if (clienteDb == null) throw new ArgumentException("Necessite de Cliente válido. Verifique o Id."); ;

            var pagamento = Pagamento.CriarPagamento(pagamentoDto.Valor, clienteDb);
            await _context.Pagamentos.AddAsync(pagamento);
            await _context.SaveChangesAsync();

            return new PagamentoDto
            {
                Id = pagamento.Id,
                ClienteId = pagamento.ClienteId,
                Valor = pagamento.Valor,
                Status = pagamento.Status.ToString(),
            };
        }



        public async Task<IEnumerable<PagamentoDto>> ObterPagamentosPorCliente(int clienteId)
        {
            var pagamentos = await _context.Pagamentos
                .Where(p => p.ClienteId == clienteId)
                .ToListAsync();

            return pagamentos.Select(p => new PagamentoDto
            {
                Id = p.Id,
                ClienteId = p.ClienteId,
                Valor = p.Valor,
                Status = p.Status.ToString()
            }).ToList();
        }
    }
}
