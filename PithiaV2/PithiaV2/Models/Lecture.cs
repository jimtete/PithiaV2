using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PithiaV2.Models;

public class Lecture
{
    [Key] public int Id { get; set; }
    
    [Column(TypeName = "jsonb")]
    public Professor Professor { get; set; }

    
    [Column(TypeName = "jsonb")]
    public Course Course { get; set; }
    
    [Required] public int? ProfessorId { get; set; }
    [Required] public int? CourseId { get; set; }
}