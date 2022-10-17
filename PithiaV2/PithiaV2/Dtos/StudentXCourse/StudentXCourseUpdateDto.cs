using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.StudentXCourse;

public class StudentXCourseUpdateDto
{
    
    [Required] public int StudentId { get; set; }
    [Required] public int CourseId { get; set; }
    
}