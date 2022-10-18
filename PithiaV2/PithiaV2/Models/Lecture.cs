using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Models;

public class Lecture
{
    [Key] public int Id { get; set; }
    
    [Required] public Professor Professor { get; set; }
    [Required] public Course Course { get; set; }
    
    [Required] public int? ProfessorId { get; set; }
    [Required] public int? CourseId { get; set; }
}