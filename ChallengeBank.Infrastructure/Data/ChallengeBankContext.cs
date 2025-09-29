using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ChallengeBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChallengeBank.Infrastructure.Data
{
    public class ChallengeBankContext : DbContext
    {
        public DbSet<Conta> Contas => Set<Conta>();
        public DbSet<ContaDesativadaLog> ContaDesativadaLog { get; set; }

        public ChallengeBankContext(DbContextOptions<ChallengeBankContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Conta>()
              .HasMany(c => c.LogsDesativacao)  
              .WithOne(l => l.Conta)           
              .HasForeignKey(l => l.DocumentoConta)               
              .HasPrincipalKey(c => c.Documento);

            modelBuilder.Entity<Conta>()
                .HasIndex(c => c.Documento)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}




