using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.Lesson;

public class LessonUpdateDto
{
    [Key]
    public int Id { get; set; }

    public string? Day { get; set; }

    public int Hours { get; set; }

    public int Minutes { get; set; }

    public string? Time { get; set; }
}