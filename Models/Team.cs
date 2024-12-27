using System.Text.Json.Serialization;

namespace PokemonTeamManager.Models;

public class Team
{
    public int Id { get; set; }
    public string? TeamName { get; set; }
    public string? TrainerName { get; set; }

    [JsonIgnore]

    public List<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
}