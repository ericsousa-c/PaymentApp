using Microsoft.AspNetCore.Mvc;
using PaymentApp.Application.DTOs;
using PaymentApp.Application.Interfaces.Services;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        
        private readonly ILogger<ClientesController> _logger;
        private readonly IClienteService _clienteService;

        public ClientesController(
                                    ILogger<ClientesController> logger,
                                    IClienteService clienteService
                                    )
        {
            _logger = logger;
            _clienteService = clienteService;   
        }

        [HttpGet(Name = "clientes")]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> RetornaClientes()
        {
            var clientes = await _clienteService.obterTodosClientes();

            if (clientes == null || !clientes.Any())
            {
                return NoContent();
            }

            return Ok(clientes);
        }

        [HttpPost(Name = "clientes")]
        public async Task<ActionResult> criaCliente([FromBody] ClienteDTO cliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cliente.Nome))
                {
                    return BadRequest("Nome é obrigatório.");
                }

                if (string.IsNullOrWhiteSpace(cliente.Email))
                {
                    return BadRequest("Email é obrigatório.");
                }

                var clienteCriado = await _clienteService.CriarCliente(cliente.Nome, cliente.Email);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro no Servidor. Contacte o Adm e informe a mensagem: {ex.Message}");
            }
        }


    }
    
}
