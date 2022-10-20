using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.PassingGrade;

public class PassingGradeCreateDto : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Grade < 5.0 || Grade > 10.0)
        {
            yield return new ValidationResult($"Grade must be between 5 and 10, you gave me {Grade}");
        }
    }

    [Required] public int GradingBookletId { get; set; }
    [Required] public int StudentXCourseId { get; set; }
    [Required] public int ProfessorId { get; set; }
    [Required] public float Grade { get; set; }
}