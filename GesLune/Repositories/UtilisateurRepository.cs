using GesLune.Models;
using System.Net.Http.Json;

namespace GesLune.Repositories
{
    public class UtilisateurRepository
    {
        public static async Task<List<Model_Utilisateur>> GetAll()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/utilisateur");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Utilisateur>>();
        }

        public static async Task<Model_Utilisateur?> Authenticate(string username, string password)
        {
            var response = await ApiClientProvider.Client.PostAsJsonAsync("api/utilisateur/authenticate", new Model_Utilisateur(0, username, password));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Model_Utilisateur>();
        }

        public static async Task<Model_Utilisateur> Enregistrer(Model_Utilisateur model)
        {
            var response = await ApiClientProvider.Client.PostAsJsonAsync("api/utilisateur", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Model_Utilisateur>();
        }

        public static async Task<int> Delete(int Utilisateur_Id)
        {
            var response = await ApiClientProvider.Client.DeleteAsync($"api/utilisateur/{Utilisateur_Id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }
    }
}
