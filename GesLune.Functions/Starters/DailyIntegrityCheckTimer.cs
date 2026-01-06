using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace GesLune.Services.Starters;

public static class DailyIntegrityCheckTimer
{
    [Function("DailyIntegrityCheckTimer")]
    public static async Task Run(
        [TimerTrigger("0 0 3 * * *")] TimerInfo timer,
        [DurableClient] DurableTaskClient client,
        FunctionContext context)
    {
        var logger = context.GetLogger("DailyIntegrityCheckTimer");

        logger.LogInformation("Starting daily data integrity validation.");

        await client.ScheduleNewOrchestrationInstanceAsync(
            "DataIntegrityOrchestrator");

        logger.LogInformation("Data integrity orchestration started.");
    }
}
