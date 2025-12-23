using GesLune.Models;
using System.Net.Http.Json;
using System.Data;

namespace GesLune.Repositories
{
    public class ArticleRepository
    {
        public static async Task<List<Model_Article>> GetAll()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/article");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Article>>();
        }

        public static async Task<List<string>> GetCodes(int article_id)
        {
            var response = await ApiClientProvider.Client.GetAsync($"api/article/codes/{article_id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<string>>();
        }

        public static async Task<DataTable> GetEtatStock()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/article/etat-stock");
            response.EnsureSuccessStatusCode();
            // Assuming the API returns a JSON that can be converted to DataTable
            var data = await response.Content.ReadFromJsonAsync<List<Dictionary<string, object>>>();
            return ConvertToDataTable(data);
        }

        public static async Task<DataTable> GetFicheStock(int Article_Id)
        {
            var response = await ApiClientProvider.Client.GetAsync($"api/article/fiche-stock/{Article_Id}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<List<Dictionary<string, object>>>();
            return ConvertToDataTable(data);
        }

        public static async Task<Model_Article> Enregistrer(Model_Article model)
        {
            var response = await ApiClientProvider.Client.PostAsJsonAsync("api/article", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Model_Article>();
        }

        public static async Task<int> AddCode(int Article_Id, string Article_Code)
        {
            var payload = new { Article_Id, Article_Code };
            var response = await ApiClientProvider.Client.PostAsJsonAsync("api/article/add-code", payload);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public static async Task<int> Delete(int id)
        {
            var response = await ApiClientProvider.Client.DeleteAsync($"api/article/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public static async Task<int> DeleteCode(string code)
        {
            var response = await ApiClientProvider.Client.DeleteAsync($"api/article/delete-code/{code}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        private static DataTable ConvertToDataTable(List<Dictionary<string, object>> data)
        {
            var dataTable = new DataTable();
            if (data == null || data.Count == 0)
                return dataTable;

            // Add columns based on first row
            foreach (var key in data[0].Keys)
            {
                dataTable.Columns.Add(key);
            }

            // Add rows
            foreach (var item in data)
            {
                var row = dataTable.NewRow();
                foreach (var key in item.Keys)
                {
                    row[key] = item[key] ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
