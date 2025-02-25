using Microsoft.AspNetCore.Mvc;
using PaymentApp.Application.DTOs;
using PaymentApp.Application.Interfaces.Services;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosController : ControllerBase
    {
        
        private readonly ILogger<ClientesController> _logger;
        private readonly IClienteService _clienteService;
        private readonly IPagamentoService _pagamentoService;

        public PagamentosController(
                                    ILogger<ClientesController> logger,
                                    IClienteService clienteService,
                                    IPagamentoService pagamentoService
                                    )
        {
            _logger = logger;
            _clienteService = clienteService;   
            _pagamentoService = pagamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagamentoDto>>> ListarPagamentos([FromQuery] int clienteId)
        {
            var pagamentos = await _pagamentoService.ObterPagamentosPorCliente(clienteId);
            if (pagamentos == null || !pagamentos.Any())
            {
                return NotFound("Cliente não possui pagamentos.");
            }
            return Ok(pagamentos);
        }

        [HttpPost]
        public async Task<ActionResult> CriarPagamento([FromBody] CriarPagamentoDto pagamentoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pagamentoCriado = await _pagamentoService.CriarPagamento(pagamentoDto);

            return Ok(pagamentoCriado);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> AtualizarStatusPagamento(int id, [FromBody] AtualizarStatusPagamentoDto statusDto)
        {
            var atualizado = await _pagamentoService.AtualizarStatusPagamento(id, statusDto.NovoStatus);
            if (!atualizado)
            {
                return BadRequest("Pagamento não permite trocar o status.");
            }
            return NoContent();
        }


    }
    
}
