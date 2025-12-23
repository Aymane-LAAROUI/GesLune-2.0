using GesLune.Models;
using System.Net.Http.Json;
using System.Data;

namespace GesLune.Repositories
{
    public class DocumentRepository
    {
        public static async Task<List<Model_Document>> GetAll()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/document");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Document>>();
        }

        public static async Task<List<Model_Document>> GetByTypeId(int Document_Type_Id)
        {
            var response = await ApiClientProvider.Client.GetAsync($"api/document/by-type/{Document_Type_Id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Document>>();
        }

        public static async Task<List<Model_Document_Type>> GetTypes()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/document/types");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Document_Type>>();
        }

        public static async Task<List<Model_Document_Ligne>> GetLignes(int Document_Id)
        {
            var response = await ApiClientProvider.Client.GetAsync($"api/document/lignes/{Document_Id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Model_Document_Ligne>>();
        }

        public static async Task<DataTable> GetChiffreAffaireMensuel()
        {
            var response = await ApiClientProvider.Client.GetAsync("api/document/chiffre-affaire-mensuel");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<List<Dictionary<string, object>>>();
            return ConvertToDataTable(data);
        }

        public static async Task<Model_Document> Enregistrer(Model_Document model)
        {
            var response = await ApiClientProvider.Client.PostAsJsonAsync("api/document", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Model_Document>();
        }

        public static async Task<int> EnregistrerLigne(Model_Document_Ligne ligne)
        {
            var response = await ApiClientProvider.Client.PostAsJsonAsync("api/document/ligne", ligne);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public static async Task<bool> EstEncaisse(int Document_Id)
        {
            var response = await ApiClientProvider.Client.GetAsync($"api/document/est-encaisse/{Document_Id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public static async Task<int> Delete(int Document_Id)
        {
            var response = await ApiClientProvider.Client.DeleteAsync($"api/document/{Document_Id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public static async Task<int> DeleteDocumentLignes(int Document_Id)
        {
            var response = await ApiClientProvider.Client.DeleteAsync($"api/document/lignes/{Document_Id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public static async Task<int> DeleteLigne(int Document_Ligne_Id)
        {
            var response = await ApiClientProvider.Client.DeleteAsync($"api/document/ligne/{Document_Ligne_Id}");
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
