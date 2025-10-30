namespace CourseService.Contracts.Events;

public record CourseCreated(
    Guid CourseId,
    string Title,
    string Slug,
    decimal Price,
    DateTime CreatedAt);

public record CoursePublished(
    Guid CourseId,
    string Title,
    string Slug,
    decimal Price,
    DateTime PublishedAt);

public record CourseUpdated(
    Guid CourseId,
    string Title,
    string Slug,
    decimal Price,
    DateTime UpdatedAt);

public record ModuleAdded(
    Guid CourseId,
    Guid ModuleId,
    string ModuleTitle,
    int ModuleOrder,
    DateTime AddedAt);

public record ModuleRemoved(
    Guid CourseId,
    Guid ModuleId,
    string ModuleTitle,
    int ModuleOrder,
    DateTime RemovedAt);

public record TagAdded(
    Guid CourseId,
    string Tag,
    DateTime AddedAt);

public record TagRemoved(
    Guid CourseId,
    string Tag,
    DateTime RemovedAt);

public record LessonVersionPublished(
    Guid CourseId,
    Guid LessonId,
    Guid VersionId,
    string Title,
    string Content,
    DateTime PublishedAt);

public record LessonViewed(
    Guid UserId,
    Guid CourseId,
    Guid LessonId,
    DateTime ViewedAt);

public record LessonCompleted(
    Guid UserId,
    Guid CourseId,
    Guid LessonId,
    DateTime CompletedAt);

// Inbound events from MediaService
public record VideoReady(
    string VideoId,
    string Provider,
    string Url,
    int DurationMinutes,
    DateTime ProcessedAt);

public record VideoFailed(
    string VideoId,
    string Provider,
    string ErrorMessage,
    DateTime FailedAt); 
