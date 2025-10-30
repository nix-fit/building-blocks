using System.Text.Json;

namespace Nix.EnrollmentService.Contracts.Dtos;

/// <summary>
/// Запрос на выдачу прав
/// </summary>
public sealed record GrantEntitlementsRequest(
    Guid TenantId,
    Guid UserId,
    string ResourceType,
    Guid ResourceId,
    IReadOnlyList<string> Actions,
    DateTimeOffset? StartAt,
    DateTimeOffset? EndAt,
    string Source,
    JsonDocument? Attrs);

/// <summary>
/// Запрос на отзыв прав
/// </summary>
public sealed record RevokeEntitlementsRequest(
    Guid TenantId,
    Guid UserId,
    string ResourceType,
    Guid ResourceId,
    IReadOnlyList<string> Actions,
    string Reason);

/// <summary>
/// Запрос на проверку прав (быстрый путь)
/// </summary>
public sealed record CheckRequest(
    Guid TenantId,
    Guid UserId,
    string ResourceType,
    Guid ResourceId,
    string Action);

/// <summary>
/// Ответ на проверку прав
/// </summary>
public sealed record CheckResponse(
    bool Allow,
    int Ttl,
    string Reason);

/// <summary>
/// Запрос на принятие решения (PDP)
/// </summary>
public sealed record DecideRequest(
    Guid TenantId,
    JsonDocument Subject,
    JsonDocument Resource,
    string Action,
    JsonDocument? Context);

/// <summary>
/// Ответ на принятие решения
/// </summary>
public sealed record DecideResponse(
    bool Allow,
    int Ttl,
    string Reason,
    Guid? PolicyId,
    int? PolicyVersion);

/// <summary>
/// Запрос на применение шаблона прав
/// </summary>
public sealed record ApplyTemplateRequest(
    Guid TenantId,
    Guid UserId,
    Guid TemplateId,
    JsonDocument Params);

/// <summary>
/// Эффективные права пользователя для ресурса
/// </summary>
public sealed record EffectiveEntitlementsResponse(
    IReadOnlyList<string> Actions,
    TierInfo? Tier,
    DateTimeOffset? ExpiresAt);

/// <summary>
/// Информация о тире
/// </summary>
public sealed record TierInfo(
    string Code,
    string Label,
    int Order);

/// <summary>
/// Запрос на создание плана тиров
/// </summary>
public sealed record CreatePlanRequest(
    Guid TenantId,
    string Name,
    bool IsDefault);

/// <summary>
/// Запрос на создание элемента плана
/// </summary>
public sealed record CreatePlanItemRequest(
    string TierLabel,
    int TierOrder,
    IReadOnlyList<string> Permissions);

/// <summary>
/// Ответ с предварительным просмотром разрешений
/// </summary>
public sealed record PreviewPermissionsResponse(
    IReadOnlyList<string> Permissions);

/// <summary>
/// Информация о плане тиров
/// </summary>
public sealed record TierPlanDto(
    Guid Id,
    Guid TenantId,
    string Name,
    bool IsDefault,
    string PlanVersion,
    DateTimeOffset CreatedAt,
    IReadOnlyList<TierPlanItemDto> Items);

/// <summary>
/// Элемент плана тиров
/// </summary>
public sealed record TierPlanItemDto(
    Guid Id,
    string TierLabel,
    int TierOrder,
    IReadOnlyList<string> Permissions);

/// <summary>
/// Информация о шаблоне тира
/// </summary>
public sealed record TierTemplateDto(
    Guid Id,
    string Label,
    Guid? TenantId,
    int? SuggestedOrder,
    IReadOnlyList<string> SuggestedActions,
    bool IsGlobal);

/// <summary>
/// Запрос на создание шаблона тира
/// </summary>
public sealed record CreateTierTemplateRequest(
    string Label,
    IReadOnlyList<string> SuggestedActions,
    int? SuggestedOrder);

/// <summary>
/// Запрос на обновление шаблона тира
/// </summary>
public sealed record UpdateTierTemplateRequest(
    string Label,
    IReadOnlyList<string> SuggestedActions,
    int? SuggestedOrder);

/// <summary>
/// Запрос на создание плана из шаблонов
/// </summary>
public sealed record CreatePlanFromTemplatesRequest(
    Guid TenantId,
    string Name,
    IReadOnlyList<Guid> TemplateIds,
    bool IsDefault);

/// <summary>
/// Информация о фиче (справочник)
/// </summary>
public sealed record FeatureDto(
    string Code,
    string Description);

/// <summary>
/// Политика фичи курса
/// </summary>
public sealed record CourseFeaturePolicyDto(
    Guid TenantId,
    Guid CourseId,
    string FeatureCode,
    string RequiredAction,
    bool Enabled);

/// <summary>
/// Запрос на создание/обновление политики фичи курса
/// </summary>
public sealed record CreateOrUpdateFeaturePolicyRequest(
    string RequiredAction,
    bool Enabled);





























