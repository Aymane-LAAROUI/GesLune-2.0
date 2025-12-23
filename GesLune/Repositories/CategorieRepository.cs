using GesLune.Models;
using System.Net.Http.Json;

namespace GesLune.Repositories
{
    public class CategorieRepository
    {
        public static async Task<List<Model_Categorie>> GetAll()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/categorie");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Categorie>>();
        }

        public static async Task<Model_Categorie> GetById(int id)
        {
            var response = await ApiClientProvider.Client.GetAsync($"api/categorie/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Model_Categorie>();
        }

        public static async Task<int> Delete(int id)
        {
            var response = await ApiClientProvider.Client.DeleteAsync($"api/categorie/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public static async Task<Model_Categorie> Enregistrer(Model_Categorie model)
        {
            var response = await ApiClientProvider.Client.PostAsJsonAsync("api/categorie", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Model_Categorie>();
        }
    }
}
