using Dapper;
using GesLune.Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GesLune.Services.Activities;

public class ValidatePaymentsWithoutDocuments
{
    private readonly IConfiguration _config;

    public ValidatePaymentsWithoutDocuments(IConfiguration config)
    {
        _config = config;
    }

    [Function("ValidatePaymentsWithoutDocuments")]
    public async Task<List<IntegrityIssue>> Run(
        [ActivityTrigger] object _)
    {
        var cs =
        Environment.GetEnvironmentVariable("SQLCONNSTR_DefaultConnection")
        ?? _config.GetConnectionString("DefaultConnection")  // local fallback
        ?? throw new InvalidOperationException("SQL connection string not found.");

        using var conn = new SqlConnection(
            cs);

        var ids = await conn.QueryAsync<int>(@"
            SELECT p.Paiement_Id
            FROM Tble_Paiements p
            LEFT JOIN Tble_Documents d ON d.Document_Id = p.Paiement_Document_Id
            WHERE d.Document_Id IS NULL
        ");

        return ids.Select(id =>
            new IntegrityIssue(
                "PaymentsWithoutDocuments",
                "Paiement",
                id,
                "High",
                "Payment is not associated with any document"
            )).ToList();
    }
}
