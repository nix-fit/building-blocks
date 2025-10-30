using Nix.BuildingBlocks.Domain;

namespace Nix.EnrollmentService.Contracts.Events;

/// <summary>
/// Событие: урок завершён
/// Публикуется ProgressService → подписывается EnrollmentService
/// </summary>
public sealed class LessonCompletedEvent : DomainEvent
{
    public Guid UserId { get; }
    public Guid LessonId { get; }
    public Guid CourseId { get; }
    public Guid TenantId { get; }
    public DateTimeOffset CompletedAt { get; }
    public int? Score { get; }

    public LessonCompletedEvent(
        Guid userId,
        Guid lessonId,
        Guid courseId,
        Guid tenantId,
        DateTimeOffset completedAt,
        int? score = null)
    {
        UserId = userId;
        LessonId = lessonId;
        CourseId = courseId;
        TenantId = tenantId;
        CompletedAt = completedAt;
        Score = score;
    }
}

/// <summary>
/// Событие: домашка сдана и проверена
/// Публикуется ProgressService → подписывается EnrollmentService
/// </summary>
public sealed class HomeworkCompletedEvent : DomainEvent
{
    public Guid UserId { get; }
    public Guid HomeworkId { get; }
    public Guid LessonId { get; }
    public Guid CourseId { get; }
    public Guid TenantId { get; }
    public DateTimeOffset CompletedAt { get; }
    public bool Passed { get; }
    public int? Score { get; }

    public HomeworkCompletedEvent(
        Guid userId,
        Guid homeworkId,
        Guid lessonId,
        Guid courseId,
        Guid tenantId,
        DateTimeOffset completedAt,
        bool passed,
        int? score = null)
    {
        UserId = userId;
        HomeworkId = homeworkId;
        LessonId = lessonId;
        CourseId = courseId;
        TenantId = tenantId;
        CompletedAt = completedAt;
        Passed = passed;
        Score = score;
    }
}

/// <summary>
/// Событие: тест пройден
/// Публикуется ProgressService → подписывается EnrollmentService
/// </summary>
public sealed class TestPassedEvent : DomainEvent
{
    public Guid UserId { get; }
    public Guid TestId { get; }
    public Guid CourseId { get; }
    public Guid TenantId { get; }
    public DateTimeOffset CompletedAt { get; }
    public int Score { get; }
    public int MaxScore { get; }

    public TestPassedEvent(
        Guid userId,
        Guid testId,
        Guid courseId,
        Guid tenantId,
        DateTimeOffset completedAt,
        int score,
        int maxScore)
    {
        UserId = userId;
        TestId = testId;
        CourseId = courseId;
        TenantId = tenantId;
        CompletedAt = completedAt;
        Score = score;
        MaxScore = maxScore;
    }
}

/// <summary>
/// Событие: модуль завершён полностью
/// Публикуется ProgressService → подписывается EnrollmentService
/// </summary>
public sealed class ModuleCompletedEvent : DomainEvent
{
    public Guid UserId { get; }
    public Guid ModuleId { get; }
    public Guid CourseId { get; }
    public Guid TenantId { get; }
    public DateTimeOffset CompletedAt { get; }

    public ModuleCompletedEvent(
        Guid userId,
        Guid moduleId,
        Guid courseId,
        Guid tenantId,
        DateTimeOffset completedAt)
    {
        UserId = userId;
        ModuleId = moduleId;
        CourseId = courseId;
        TenantId = tenantId;
        CompletedAt = completedAt;
    }
}











