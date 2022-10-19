using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.GradingBooklet;

public class GradingBookletUpdateDto
{
    
    [Required]
    public float GradingSum { get; set; }
    [Required]
    public int PassedCourses { get; set; }
    
}