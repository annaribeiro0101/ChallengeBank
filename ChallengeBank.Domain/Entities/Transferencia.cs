using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeBank.Domain.Entities
{
    public class Transferencia
    {
        public string DocumentoOrigem { get; set; }
        public string DocumentoDestino { get; set; }
        public decimal Valor { get; set; }
    }
}
