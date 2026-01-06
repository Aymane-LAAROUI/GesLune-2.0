namespace GesLune.Functions.Models;

public record IntegrityIssue(
    string RuleName,
    string EntityType,
    int EntityId,
    string Severity,
    string Description
);
