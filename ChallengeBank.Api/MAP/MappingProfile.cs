using AutoMapper;
using ChallengeBank.Api.NovaPasta;
using ChallengeBank.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChallengeBank.Api.MAP
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             CreateMap<Conta, ContaDto>();

             CreateMap<ContaDesativadaLog, ContaDesativadaLogDto>();
        }
    }
}