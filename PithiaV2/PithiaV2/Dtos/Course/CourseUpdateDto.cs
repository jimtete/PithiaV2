using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.Course;

public class CourseUpdateDto
{
    
    [Required]
    [MaxLength(5)]
    public string? CourseCharacteristic { get; set; }

    public int TheoryHours { get; set; }
    
    public int LabHours { get; set; }
    
    
    
    
}