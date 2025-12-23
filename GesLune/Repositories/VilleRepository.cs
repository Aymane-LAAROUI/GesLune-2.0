using GesLune.Models;
using System.Net.Http.Json;

namespace GesLune.Repositories
{
    public class VilleRepository
    {
        public static async Task<List<Model_Ville>> GetAll()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/ville");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Ville>>();
        }
    }
}
