using AutoMapper;
using ChallengeBank.Api.NovaPasta;
using ChallengeBank.Application.Services;
using ChallengeBank.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeBank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
        private readonly ContaService _service;
        private readonly IMapper _mapper;

        public ContasController(ContaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] ContaRequest request)
        {
            try
            {
                var conta = await _service.CadastrarAsync(request.NomeCliente, request.Documento);
                return Ok(conta);
            }
            catch (InvalidOperationException ex)
            {           
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception)
            {       
                return StatusCode(500, new { erro = "Ocorreu um erro interno no servidor." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Consultar([FromQuery] string? nome, [FromQuery] string? documento)
        {
            var contas = await _service.ConsultarAsync(nome, documento);
            var contasDto = _mapper.Map<IEnumerable<ContaDto>>(contas);

            if (!contas.Any())
                return NotFound("Nenhuma conta encontrada com os filtros informados.");

            return Ok(contasDto);
        }

        [HttpPut("inativar/{documento}")]
        public async Task<IActionResult> Inativar(string documento)
        {
            await _service.InativarAsync(documento);
            return NoContent();
        }

        [HttpPost("transferir")]
        public async Task<IActionResult> Transferir([FromBody] Transferencia request)
        {
            try
            {
                await _service.TransferirAsync(
                    request.DocumentoOrigem,
                    request.DocumentoDestino,
                    request.Valor
                );

                 return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
    public record ContaRequest(string NomeCliente, string Documento);
}