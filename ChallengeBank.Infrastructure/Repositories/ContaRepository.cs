
using Microsoft.EntityFrameworkCore;
using ChallengeBank.Domain.Entities;
using ChallengeBank.Domain.Interfaces;
using ChallengeBank.Infrastructure.Data;

namespace ChallengeBank.Infrastructure.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly ChallengeBankContext _context;
        public ContaRepository(ChallengeBankContext context) => _context = context;

        public async Task AddAsync(Conta conta) => await _context.Contas.AddAsync(conta);

        public async Task<Conta?> GetByDocumentoAsync(string documento) =>
            await _context.Contas.FirstOrDefaultAsync(c => c.Documento == documento);

        public async Task<IEnumerable<Conta>> GetByFiltroAsync(string? nome, string? documento)
        {
            var query = _context.Contas.AsQueryable().Include(c => c.LogsDesativacao).AsQueryable();


            if (!string.IsNullOrEmpty(nome))
            {
                var nomeBusca = nome.ToLower();
                query = query.Where(c => c.NomeCliente.ToLower().Contains(nomeBusca));
            }
            if (!string.IsNullOrEmpty(documento))
            {
                query = query.Where(c => c.Documento == documento);
            }
            return await query.ToListAsync();
        }

        public Task UpdateAsync(Conta conta)
        {
            _context.Contas.Update(conta);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public Task TransferirAsync(string documentoOrigem, string documentoDestino, decimal valor)
        {
            throw new NotImplementedException();
        }

        public async Task AddDesativacaoLog(ContaDesativadaLog log)
        {
             await _context.ContaDesativadaLog.AddAsync(log);

        }
    }
}
