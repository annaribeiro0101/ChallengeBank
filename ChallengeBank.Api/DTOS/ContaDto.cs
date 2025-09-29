namespace ChallengeBank.Api.NovaPasta
{
    public class ContaDto
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string Documento { get; set; }
        public decimal Saldo { get; set; }
        public DateTime DataAbertura { get; set; }
        public bool Ativa { get; set; }
        public List<ContaDesativadaLogDto> LogsDesativacao { get; set; }

    }
}
   