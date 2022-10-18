using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.Professor;

public class ProfessorUpdateDto : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Rank > 4 || Rank < 0)
        {
            yield return new ValidationResult($"Rank must be between 0 and 4. You gave me {Rank}");
        }
    }


    public int? Rank { get; set; }

    public float Salary { get; set; }
    
    
}