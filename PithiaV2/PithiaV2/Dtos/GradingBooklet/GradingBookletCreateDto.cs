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

        if (GradingSum != 0)
        {
            yield return new ValidationResult($"Grading sum on creation must be 0, you gave me {GradingSum}");
        }

        if (PassedCourses != 0)
        {
            yield return new ValidationResult($"Passed courses on creation must be 0, you gave me {PassedCourses}");
        }
        
    }

    [Required]
    public int UserId { get; set; }
    
    [Required]
    public float GradingSum { get; set; }
    
    [Required]
    public int PassedCourses { get; set; }
}