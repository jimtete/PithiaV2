namespace PithiaV2.Dtos.Lesson;

public class LessonReadDto
{
    public int Id { get; set; }

    public int LectureId { get; set; }

    public string? Day { get; set; }

    public int Hours { get; set; }

    public int Minutes { get; set; }

    public string? Time { get; set; }
}