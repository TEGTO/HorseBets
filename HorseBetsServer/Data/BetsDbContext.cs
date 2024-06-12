using HorseBets.Bets.Models;
using HorseBetsServer.Bets.Models;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Data
{
    public class BetsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<HorseCoefficient> HorseCoefficients { get; set; }

        public BetsDbContext(DbContextOptions<BetsDbContext> options) : base(options)
        {
        }
    }
}
