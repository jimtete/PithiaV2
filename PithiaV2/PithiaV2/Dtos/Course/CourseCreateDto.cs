using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.Course;

public class CourseCreateDto : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Specialization > 3 || Specialization < 0)
        {
            yield return new ValidationResult($"Specialization must be between 0 and 3, you gave me {Specialization}");
        }

        if (TheoryHours < 0)
        {
            yield return new ValidationResult("Theory hours must not be a negative number");
        }

        if (LabHours < 0)
        {
            yield return new ValidationResult("Lab hours must not be a negative number");
        }

        if (LabHours + TheoryHours == 0)
        {
            yield return new ValidationResult(
                $"Total hours MUST be a positive number, you gave {LabHours + TheoryHours}");
        }

        if (LabHours + TheoryHours > 40)
        {
            yield return new ValidationResult("You gave an incorrent amount of hours");
        }

        if (string.IsNullOrEmpty(CourseName))
        {
            yield return new ValidationResult("You gave an incorrect course name");
        }

        if (string.IsNullOrEmpty(CourseCharacteristic))
        {
            yield return new ValidationResult("You gave an incorrect course characteristic");
        }
    }

    [Required]
    [MaxLength(5)]
    public string? CourseCharacteristic { get; set; }

    [Required]
    public string? CourseName { get; set; }

    [Required]
    public int Specialization { get; set; }

    [Required]
    public int TheoryHours { get; set; }

    [Required]
    public int LabHours { get; set; }

}