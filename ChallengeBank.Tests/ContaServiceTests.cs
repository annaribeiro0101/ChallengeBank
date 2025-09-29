using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeBank.Application.Services;
using ChallengeBank.Domain.Entities;
using ChallengeBank.Domain.Interfaces;
using Moq;
using Xunit;

namespace ChallengeBank.Tests
{
    public class ContaServiceTests
    {
        private readonly Mock<IContaRepository> _mockRepo;
        private readonly ContaService _service;

        public ContaServiceTests()
        {
            _mockRepo = new Mock<IContaRepository>();
            _service = new ContaService(_mockRepo.Object);
        }

 
        [Fact]
        public async Task CadastrarAsync_DeveRetornarConta_QuandoDocumentoNaoExiste()
        {
             var documentoNovo = "11122233344";

             _mockRepo.Setup(r => r.GetByDocumentoAsync(documentoNovo))
                     .ReturnsAsync((Conta)null);

             var result = await _service.CadastrarAsync("Novo Cliente", documentoNovo);

             Xunit.Assert.NotNull(result);

             Xunit.Assert.Equal(documentoNovo, result.Documento);

            
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<Conta>()), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CadastrarAsync_DeveLancarExcecao_QuandoDocumentoJaExiste()
        {
             var documentoExistente = "10043247660";

             _mockRepo.Setup(r => r.GetByDocumentoAsync(documentoExistente))
                     .ReturnsAsync(new Conta { Documento = documentoExistente });

            
            await Xunit.Assert.ThrowsAsync<System.InvalidOperationException>(
                () => _service.CadastrarAsync("Cliente Duplicado", documentoExistente)
            );

             _mockRepo.Verify(r => r.AddAsync(It.IsAny<Conta>()), Times.Never);
        }

 
        [Fact]
        public async Task InativarAsync_DeveInativarConta_QuandoAtiva()
        {
             var documento = "11122233344";
            var conta = new Conta { Documento = documento, Ativa = true };

            _mockRepo.Setup(r => r.GetByDocumentoAsync(documento)).ReturnsAsync(conta);

             await _service.InativarAsync(documento);

             Xunit.Assert.False(conta.Ativa);

             _mockRepo.Verify(r => r.UpdateAsync(conta), Times.Once);
             Xunit.Assert.Single(conta.LogsDesativacao);
        }

         [Fact]
        public async Task InativarAsync_DeveLancarNotFound_QuandoContaNaoExiste()
        {
             var documentoInexistente = "123456";
            _mockRepo.Setup(r => r.GetByDocumentoAsync(documentoInexistente))
                     .ReturnsAsync((Conta)null);

             await Xunit.Assert.ThrowsAsync<System.Collections.Generic.KeyNotFoundException>(
                () => _service.InativarAsync(documentoInexistente)
            );
        }

     }
}
