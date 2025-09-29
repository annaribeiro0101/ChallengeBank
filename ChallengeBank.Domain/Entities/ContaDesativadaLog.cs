using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeBank.Domain.Entities
{
    public class ContaDesativadaLog
    {
        public int Id { get; set; }

        public string DocumentoConta { get; set; }

        public DateTime DataHoraDesativacao { get; set; }
       
        public string UsuarioResponsavel { get; set; }
        public Conta Conta { get; set; }

    }
}
