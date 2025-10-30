using System.ComponentModel.DataAnnotations;

namespace CourseService.Contracts.Dtos;

/// <summary>
/// Полная DTO модуля для session cache.
/// </summary>
public class ModuleDto
{
    public Guid Id { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    public int Order { get; set; }
    
    public List<LessonDto> Lessons { get; set; } = [];
} 
