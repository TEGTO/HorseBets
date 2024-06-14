using HorseBets.Api.Data;
using HorseBets.Bets.Models;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Data
{
    public class BetsDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<HorseCoefficient> HorseCoefficients { get; set; }

        public BetsDbContext(DbContextOptions<BetsDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
    public static class Extensions
    {
        public static void CreateDbIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<BetsDbContext>();
            try
            {
                context.Database.EnsureCreated();
                DevelopSeedData.SeedData(context);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
