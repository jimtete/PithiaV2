using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.Course;

public class CourseCreateDto
{
    [Required]
    [MaxLength(5)]
    public string? CourseCharacteristic { get; set; }

    [Required]
    public string? CourseName { get; set; }

    [Required]
    public int Specialization { get; set; }

    [Required]
    public int TheoryHours { get; set; }

    [Required]
    public int LabHours { get; set; }

}