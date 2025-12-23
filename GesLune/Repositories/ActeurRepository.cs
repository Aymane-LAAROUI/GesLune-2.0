using GesLune.Models;
using System.Net.Http.Json;

namespace GesLune.Repositories
{
    public class ActeurRepository
    {
        public static async Task<List<Model_Acteur>> GetAll()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/acteur");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Acteur>>();
        }

        public static async Task<List<Model_Acteur>> GetAll(int top)
        {
            var response = await ApiClientProvider.Client.GetAsync($"api/acteur/top/{top}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Acteur>>();
        }

        public static async Task<List<Model_Acteur_Type>> GetTypes()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/acteur/types");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Acteur_Type>>();
        }

        public static async Task<List<Model_Acteur>> GetByTypeId(int acteurTypeId)
        {
            var response = await ApiClientProvider.Client.GetAsync($"api/acteur/by-type/{acteurTypeId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Acteur>>();
        }

        public static async Task<object> GetReleve(int acteurId, DateTime? dateDu = null, DateTime? dateAu = null)
        {
            string url = $"api/acteur/releve/{acteurId}";
            if (dateDu.HasValue || dateAu.HasValue)
            {
                var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
                if (dateDu.HasValue) query["dateDu"] = dateDu.Value.ToString("o");
                if (dateAu.HasValue) query["dateAu"] = dateAu.Value.ToString("o");
                url += "?" + query.ToString();
            }
            var response = await ApiClientProvider.Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            // Adapter le type de retour selon le modèle attendu
            return await response.Content.ReadFromJsonAsync<object>();
        }

            public static async Task<Model_Acteur> Enregistrer(Model_Acteur model)
            {
                var response = await ApiClientProvider.Client.PostAsJsonAsync("api/acteur", model);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Model_Acteur>();
            }

        public static async Task<int> Delete(int acteurId)
        {
            var response = await ApiClientProvider.Client.DeleteAsync($"api/acteur/{acteurId}");
            return int.TryParse(response.Content.ToString(), out var result) ? result : 0;
        }
    }
}
