using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Models;

public class StudentXCourse
{
    
    [Key] public int Id { get; set; }
    [Required] public int StudentId { get; set; }
    [Required] public int CourseId { get; set; }
    
}