using GesLune.Models;
using System.Net.Http.Json;

namespace GesLune.Repositories
{
    public class PaiementRepository
    {
        public static async Task<List<Model_Paiement>> GetAll()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/paiement");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Paiement>>();
        }

        public static async Task<List<Model_Paiement_Type>> GetTypes()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/paiement/types");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Paiement_Type>>();
        }

        public static async Task<int> Delete(int id)
        {
            var response = await ApiClientProvider.Client.DeleteAsync($"api/paiement/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public static async Task<Model_Paiement> Enregistrer(Model_Paiement model)
        {
            var response = await ApiClientProvider.Client.PostAsJsonAsync("api/paiement", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Model_Paiement>();
        }
    }
}
