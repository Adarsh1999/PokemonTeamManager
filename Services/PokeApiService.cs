using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokemonTeamManager.Services
{
    public class PokeApiService
    {
        private readonly HttpClient _httpClient;

        public PokeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Update the method to return nullable PokemonApiResponse
        public async Task<PokemonApiResponse?> GetPokemonAsync(string nameOrId)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{nameOrId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                // Deserialize and return the response, may be null
                return JsonConvert.DeserializeObject<PokemonApiResponse>(jsonResponse);
            }
            // Return null if the API call fails
            return null;
        }
    }

    // Mark Name and Types as nullable
    public class PokemonApiResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }  // Nullable string to avoid warnings
        public List<PokemonType>? Types { get; set; }  // Nullable list to avoid warnings
    }

    // Mark Type as nullable
    public class PokemonType
    {
        public PokemonTypeDetail? Type { get; set; }  // Nullable Type to avoid warnings
    }

    // Mark Name as nullable
    public class PokemonTypeDetail
    {
        public string? Name { get; set; }  // Nullable string to avoid warnings
    }
}