using GesLune.Api.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace GesLune.Api.Repositories
{
    public class VilleRepository
    {
        public static List<Model_Ville> GetAll()
        {
            using SqlConnection connection = new(MainRepository.ConnectionString);
            return connection.Query<Model_Ville>("SELECT * FROM Tble_Villes").ToList();
        }
    }
}
