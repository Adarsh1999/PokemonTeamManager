using Microsoft.EntityFrameworkCore;

namespace PokemonTeamManager.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
    }
}