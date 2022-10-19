using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Models;

public class GradingBooklet
{
    [Key] public int Id { get; set; }
    
    [Required] public User User { get; set; }
    [Required] public int UserId { get; set; }
    
    [Required] public float GradingSum { get; set; }
    [Required] public int PassedCourses { get; set; }
}