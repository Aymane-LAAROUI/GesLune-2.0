using GesLune.Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace GesLune.Services.Orchestrators;

public static class DataIntegrityOrchestrator
{
    [Function("DataIntegrityOrchestrator")]
    public static async Task Run(
        [OrchestrationTrigger] TaskOrchestrationContext context)
    {
        var issues = new List<IntegrityIssue>();

        issues.AddRange(await context.CallActivityAsync<List<IntegrityIssue>>(
            "ValidateDocumentsWithoutPayments"));

        issues.AddRange(await context.CallActivityAsync<List<IntegrityIssue>>(
            "ValidatePaymentsWithoutDocuments"));

        //issues.AddRange(await context.CallActivityAsync<List<IntegrityIssue>>(
        //    "ValidateDocumentTotals"));

        await context.CallActivityAsync(
            "EmitValidationTelemetry", issues);
    }
}
