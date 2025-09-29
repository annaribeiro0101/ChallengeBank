using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeBank.Domain.Entities;

namespace ChallengeBank.Domain.Interfaces
{
    public interface IContaRepository
    {
        Task<Conta?> GetByDocumentoAsync(string documento);
        Task<IEnumerable<Conta>> GetByFiltroAsync(string? nome, string? documento);
        Task AddAsync(Conta conta);
        Task UpdateAsync(Conta conta);
        Task SaveChangesAsync();
        Task TransferirAsync(string documentoOrigem, string documentoDestino, decimal valor);
        Task AddDesativacaoLog(ContaDesativadaLog log);

    }
}
