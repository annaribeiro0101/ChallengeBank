using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeBank.Domain.Entities
{
    public class Conta
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public decimal Saldo { get; set; } = 1000m;
        public DateTime DataAbertura { get; set; } = DateTime.UtcNow;
        public bool Ativa { get; set; } = true;
        public ICollection<ContaDesativadaLog> LogsDesativacao { get; set; } = new List<ContaDesativadaLog>();
        public void Debitar(decimal valor)
        {
            if (!Ativa)
                throw new InvalidOperationException("Conta de origem está inativa.");

            if (Saldo < valor)
                throw new InvalidOperationException("Conta de origem não tem saldo suficiente.");

            if (valor <= 0)
                throw new ArgumentException("O valor da transferência deve ser maior que zero.");

            Saldo -= valor;
        }

        public void Creditar(decimal valor)
        {
            if (!Ativa)
                throw new InvalidOperationException("Conta de destino está inativa.");

            if (valor <= 0)
                throw new ArgumentException("O valor da transferência deve ser maior que zero.");

            Saldo += valor;
        }
    }
}
