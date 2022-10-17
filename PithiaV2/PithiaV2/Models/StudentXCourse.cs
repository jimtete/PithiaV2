using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Models;

public class StudentXCourse
{
    
    [Key] public int Id { get; set; }
    [Required] public User User { get; set; }
    [Required] public Course Course { get; set; }

    public int? UserId { get; set; }
    public int? CourseId { get; set; }
    
}