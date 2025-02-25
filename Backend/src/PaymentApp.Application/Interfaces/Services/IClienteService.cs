using PaymentApp.Application.DTOs;
using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Enum;

namespace PaymentApp.Application.Interfaces.Services
{
    public interface IClienteService
    {
        Task<Cliente> CriarCliente(string nome, string email);
        Task<bool> AtualizarNome(int Id, string novoNome);
        Task<IEnumerable<ClienteDTO>> obterTodosClientes();

    }

}


