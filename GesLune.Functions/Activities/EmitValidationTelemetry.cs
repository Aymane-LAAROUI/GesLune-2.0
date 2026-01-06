using GesLune.Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GesLune.Services.Activities;

public class EmitValidationTelemetry
{
    private readonly ILogger _logger;

    public EmitValidationTelemetry(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<EmitValidationTelemetry>();
    }

    [Function("EmitValidationTelemetry")]
    public void Run(
        [ActivityTrigger] List<IntegrityIssue> issues)
    {
        _logger.Log(LogLevel.Information, $"--- Data Integrity check {DateTime.Now}, results :");
        if (issues.Count < 1)
        {
            _logger.Log(LogLevel.Information, "0 issues!");
            return;
        }
        foreach (var issue in issues)
        {
            _logger.LogWarning(
                "IntegrityIssue detected",
                new Dictionary<string, object>
                {
                    ["RuleName"] = issue.RuleName,
                    ["EntityType"] = issue.EntityType,
                    ["EntityId"] = issue.EntityId,
                    ["Severity"] = issue.Severity,
                    ["Description"] = issue.Description
                });
        }
    }
}
