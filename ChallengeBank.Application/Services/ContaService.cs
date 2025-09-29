using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeBank.Domain.Entities;
using ChallengeBank.Domain.Interfaces;

namespace ChallengeBank.Application.Services
{
    public class ContaService
    {
        private readonly IContaRepository _repo;

        public ContaService(IContaRepository repo)
        {
            _repo = repo;
        }

        public async Task<Conta> CadastrarAsync(string nome, string documento)
        {
            var existente = await _repo.GetByDocumentoAsync(documento);
            if (existente != null)
            {
                throw new InvalidOperationException("Já existe conta para este documento.");
            }
            var conta = new Conta { NomeCliente = nome, Documento = documento };
            await _repo.AddAsync(conta);
            await _repo.SaveChangesAsync();
            return conta;
        }

        public async Task<IEnumerable<Conta>> ConsultarAsync(string? nome, string? documento) =>
            await _repo.GetByFiltroAsync(nome, documento);
  
      
        public async Task TransferirAsync(string documentoOrigem, string documentoDestino, decimal valor)
        {
            var contaOrigem = await _repo.GetByDocumentoAsync(documentoOrigem)
                ?? throw new KeyNotFoundException("Conta de origem não encontrada.");

            var contaDestino = await _repo.GetByDocumentoAsync(documentoDestino)
                ?? throw new KeyNotFoundException("Conta de destino não encontrada.");

          
            contaOrigem.Debitar(valor);
            contaDestino.Creditar(valor);

           
            await _repo.UpdateAsync(contaOrigem);
            await _repo.UpdateAsync(contaDestino);

            await _repo.SaveChangesAsync();
        }

        public async Task InativarAsync(string documento)
        {
            var conta = await _repo.GetByDocumentoAsync(documento)
                ?? throw new KeyNotFoundException("Conta não encontrada.");

            if (!conta.Ativa)
                throw new InvalidOperationException("Conta já está inativa.");

     
            conta.Ativa = false;
            await _repo.UpdateAsync(conta);
        

            var desativacaoLog = new ContaDesativadaLog
            {
                DocumentoConta = documento,
                DataHoraDesativacao = DateTime.UtcNow,                                                      
                UsuarioResponsavel = "Usuario Admin"
            };

             await _repo.AddDesativacaoLog(desativacaoLog);

             await _repo.SaveChangesAsync();
        }
    }
}
