using System.ComponentModel.DataAnnotations;

namespace CourseService.Contracts.Dtos;

/// <summary>
/// Полная DTO урока для session cache.
/// </summary>
public class LessonDto
{
    public Guid Id { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Slug { get; set; } = string.Empty;
    
    public int Position { get; set; }
    
    public bool IsDraft { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public LessonVersionDto? LatestVersion { get; set; }
    
    public LessonVersionDto? PublishedVersion { get; set; }
    
    public List<LessonVersionDto> Versions { get; set; } = [];
}

/// <summary>
/// DTO версии урока с полным контентом.
/// </summary>
public class LessonVersionDto
{
    public Guid Id { get; set; }
    
    public int Version { get; set; }
    
    public string Content { get; set; } = string.Empty;
    
    public Guid AuthorId { get; set; }
    
    public bool IsDraft { get; set; }
    
    public DateTime CreatedAt { get; set; }
} 
