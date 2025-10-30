namespace Nix.EnrollmentService.Contracts.Dtos;

// DTO types для high performance endpoints
public sealed record BulkGrantRequest(IReadOnlyList<GrantEntitlementsRequest> Requests);
public sealed record BulkGrantResponse(bool Success, int RequestsProcessed, int EntitlementsCreated, long ProcessingTimeMs);
public sealed record FastCheckRequest(Guid TenantId, Guid UserId, string ResourceType, Guid ResourceId, string Action);
public sealed record BatchCheckRequest(Guid TenantId, Guid UserId, string ResourceType, Guid ResourceId, IReadOnlyList<string> Actions);






































