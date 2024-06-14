using HorseBets.Bets.Models;
using HorseBets.Data;

namespace HorseBets.Api.Data
{
    public class DevelopSeedData
    {
        public static void SeedData(BetsDbContext context)
        {
            if (context.Horses.Any())
                return;
            List<Horse> horses = new List<Horse>()
            {
                new Horse { Id = Guid.NewGuid().ToString(), Speed = 1f, Name = "Horse1" },
                new Horse { Id = Guid.NewGuid().ToString(), Speed = 2f, Name = "Horse2" },
                new Horse { Id = Guid.NewGuid().ToString(), Speed = 0.5f, Name = "Horse3" },
            };
            context.AddRange(horses);
            context.SaveChanges();
        }
    }
}
