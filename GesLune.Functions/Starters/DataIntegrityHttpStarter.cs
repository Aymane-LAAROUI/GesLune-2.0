using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace GesLune.Services.Starters;

public static class DataIntegrityHttpStarter
{
    [Function("StartDataIntegrityValidation")]
    public static async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function,"get", "post")] HttpRequestData request,
        [DurableClient] DurableTaskClient client,
        FunctionContext context)   // ✅ Use context to get logger
    {
        var logger = context.GetLogger("StartDataIntegrityValidation");

        var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
            "DataIntegrityOrchestrator");

        logger.LogInformation("Started data integrity orchestration. InstanceId: {InstanceId}", instanceId);

        var response = request.CreateResponse(HttpStatusCode.Accepted);
        await response.WriteStringAsync(
            $"Orchestration started. InstanceId: {instanceId}",
            Encoding.UTF8);
        return response;
    }
}
