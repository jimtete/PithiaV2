using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.GradingBooklet;

public class GradingBookletCreateDto : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!(UserId > 0))
        {
            yield return new ValidationResult("Invalid user id");
        }
        
        
    }

    [Required]
    public int UserId { get; set; }
    
}