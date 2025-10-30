using System.ComponentModel.DataAnnotations;
using CourseService.Domain.Enums;
using Nix.BuildingBlocks.Domain.ValueObjects;

namespace CourseService.Contracts.Dtos;

/// <summary>
/// Полная DTO курса для session cache.
/// Содержит всю информацию включая контент уроков.
/// </summary>
public class CourseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public  CourseStatus Status { get; set; }
    public string? CoverImageUrl { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<string> Tags { get; set; } = [];
    public List<ModuleDto> Modules { get; set; } = [];
}

/// <summary>
/// Легкая DTO курса для warm cache.
/// Содержит только базовую информацию без контента уроков.
/// </summary>
public class CourseLightDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public CourseStatus Status { get; set; }
    public string? CoverImageUrl { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<string> Tags { get; set; } = [];
    
    /// <summary>
    /// Структура курса без контента уроков.
    /// </summary>
    public List<ModuleLightDto> Modules { get; set; } = [];
}

/// <summary>
/// Легкая DTO модуля для warm cache.
/// </summary>
public class ModuleLightDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<LessonLightDto> Lessons { get; set; } = [];
}

/// <summary>
/// Легкая DTO урока для warm cache.
/// Содержит только метаданные без контента.
/// </summary>
public class LessonLightDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int Position { get; set; }
    public bool IsDraft { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// Количество версий урока (без самого контента).
    /// </summary>
    public int VersionsCount { get; set; }
    
    /// <summary>
    /// Последняя версия урока (только метаданные).
    /// </summary>
    public int? LatestVersion { get; set; }
} 
