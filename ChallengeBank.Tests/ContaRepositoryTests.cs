using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeBank.Domain.Entities;
using ChallengeBank.Infrastructure.Data;
using ChallengeBank.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using ChallengeBank.Domain.Interfaces;  
using ChallengeBank.Application.Services; 
using System.Threading.Tasks;
 
namespace ChallengeBank.Tests
{
    public class ContaRepositoryTests
    {
        private ChallengeBankContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ChallengeBankContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new ChallengeBankContext(options);
            context.Database.EnsureCreated();  
            return context;
        }

        [Fact]
        public async Task GetByFiltroAsync_DeveIncluirLogsDesativacao()
        {
            var dbName = "TestDB_Include";
            using (var context = CreateContext(dbName))
            {
                var repository = new ContaRepository(context);

                var conta = new Conta
                {
                    Documento = "11122233344",
                    NomeCliente = "Teste Log",
                    Ativa = false
                };

                conta.LogsDesativacao.Add(new ContaDesativadaLog
                {
                    DataHoraDesativacao = DateTime.Now,
                    UsuarioResponsavel = "System"
                });

                await repository.AddAsync(conta);
                await repository.SaveChangesAsync();
            }

             using (var context = CreateContext(dbName))
            {
                var repository = new ContaRepository(context);

                var contas = await repository.GetByFiltroAsync(null, "11122233344");

                var contaComLog = contas.FirstOrDefault();

                Xunit.Assert.NotNull(contaComLog);
                Xunit.Assert.Single(contaComLog.LogsDesativacao);
                Xunit.Assert.Equal("System", contaComLog.LogsDesativacao.First().UsuarioResponsavel);
            }
        }
    }
}
