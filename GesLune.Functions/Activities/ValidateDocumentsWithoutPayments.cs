using Dapper;
using GesLune.Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GesLune.Services.Activities;

public class ValidateDocumentsWithoutPayments
{
    private readonly IConfiguration _config;

    public ValidateDocumentsWithoutPayments(IConfiguration config)
    {
        _config = config;
    }

    [Function("ValidateDocumentsWithoutPayments")]
    public async Task<List<IntegrityIssue>> Run(
        [ActivityTrigger] object _)
    {
        var cs =
        Environment.GetEnvironmentVariable("SQLCONNSTR_DefaultConnection")
        ?? _config.GetConnectionString("DefaultConnection")  // local fallback
        ?? throw new InvalidOperationException("SQL connection string not found.");

        using var conn = new SqlConnection(cs);

        var ids = await conn.QueryAsync<int>(@"
            SELECT d.Document_Id
            FROM Tble_Documents d
            LEFT JOIN Tble_Paiements p ON p.Paiement_Document_Id = d.Document_Id
            WHERE p.Paiement_Id IS NULL
        ");

        return ids.Select(id =>
            new IntegrityIssue(
                "DocumentsWithoutPayments",
                "Document",
                id,
                "High",
                "Document has no associated payment"
            )).ToList();
    }
}
