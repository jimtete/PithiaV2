using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.Professor;

public class ProfessorCreateDto
{
    [Required]
    [MaxLength(64)]
    public string? FirstName { get; set; }
    
    [Required]
    [MaxLength(64)]
    public string? LastName { get; set; }
    
    [Required]
    [Range(0,4)]
    public int? Rank { get; set; }
    
    [Required] public float? Salary { get; set; }
}