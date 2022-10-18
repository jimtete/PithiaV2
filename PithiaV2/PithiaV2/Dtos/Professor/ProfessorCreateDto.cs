using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.Professor;

public class ProfessorCreateDto : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Rank > 4 || Rank < 0)
        {
            yield return new ValidationResult($"Rank must be between 0 and 4. You gave me {Rank}");
        }
    }

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