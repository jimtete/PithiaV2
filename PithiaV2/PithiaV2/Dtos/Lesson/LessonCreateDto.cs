using System.ComponentModel.DataAnnotations;
using PithiaV2.Models;

namespace PithiaV2.Dtos.Lesson;

public class LessonCreateDto : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        bool ValidDay;
        switch (Day.ToLower())
        {
            case "monday" :
                ValidDay = true;
                break;
            case "tuesday" :
                ValidDay = true;
                break;
            case "wednesday" :
                ValidDay = true;
                break;
            case "thursday" :
                ValidDay = true;
                break;
            case "friday" :
                ValidDay = true;
                break;
            case "saturday" :
                ValidDay = true;
                break;
            default:
                ValidDay = false;
                break;
        }

        if (!ValidDay)
        {
            yield return new ValidationResult($"Your day is invalid, you gave me {Day} \n");
        }

        if (Minutes < 0)
        {
            yield return new ValidationResult("Minutes field cannot be a negative number \n");
        }

        if (Hours < 0)
        {
            yield return new ValidationResult("Hours field cannot be a negative number \n");
        }
        
        int TotalTime = Hours * 60 + Minutes;

        if (TotalTime == 0)
        {
            yield return new ValidationResult("Incorrect amount of lesson's time!\n");
        }
        
        // Time validation Chapter
        
        //Checking the length
        if (Time.Length != 5)
        {
            yield return new ValidationResult($"Incorrect time value, you gave me {Time}\n");
        }
        
        //Checking the colon
        if (!Time.Substring(2, 1).Equals(":"))
        {
            yield return new ValidationResult($"Character at index 2 must be :, you gave me {Time.Substring(2, 1)}\n");
        }
        
        //Checking if Parseable
        string hours = Time.Substring(0, 2);
        string minutes = Time.Substring(3, 2);

        int parseHours, parseMinutes;

        if (!int.TryParse(hours, out parseHours))
        {
            yield return new ValidationResult($"{hours} is not parsable\n");
        }

        if (!int.TryParse(minutes, out parseMinutes))
        {
            yield return new ValidationResult($"{minutes} is not parsable\n");
        }

        if (parseHours < 0 || parseHours > 23)
        {
            yield return new ValidationResult($"{parseHours} not a valid hours value! \n");
        }

        if (parseMinutes < 0 || parseMinutes > 59)
        {
            yield return new ValidationResult($"{parseMinutes} not a valid minutes value! \n");
        }
    }
    
    [Required] public int LectureId { get; set; }
    //
    [Required]
    public string? Day { get; set; } // Monday, Tuesday, Wednesday, Thursday, Friday, Saturday

    public int Hours { get; set; }
    public int Minutes { get; set; }
    
    //
    [Required] public string? Time { get; set; } //needs validation

    
}