using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentApp.Application.DTOs;
using PaymentApp.Application.Interfaces.Services;
using PaymentApp.Domain.Entities;
using PaymentApp.Infrastructure.Context;

namespace PaymentApp.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly PaymentAppDbContext _context;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(PaymentAppDbContext context, ILogger<ClienteService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<bool> AtualizarNome(int id, string novoNome)
        {
            try
            {
                var editRecord = await _context.Clientes.FindAsync(id);

                if (editRecord == null)
                {
                    return false;
                }

                editRecord.AtualizarNome(novoNome);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar nome do cliente com ID {Id}", id);
                return false;
            }
        }

        public async Task<Cliente> CriarCliente(string nome, string email)
        {
            try
            {
                var newRecord = Cliente.CriarCliente(nome, email);
                var db = await _context.Clientes.AddAsync(newRecord);
                await _context.SaveChangesAsync();

                return await _context.Clientes.FindAsync(db);

            }
            catch (Exception ex) {
                _logger.LogError(ex, "Erro ao criar o cliente");
                return null;
            }

        }

        public async Task<IEnumerable<ClienteDTO>> obterTodosClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();

            return clientes.Select(c => new ClienteDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Email = c.Email,
            }).ToList();
        }
    }
}
