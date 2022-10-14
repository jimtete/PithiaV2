namespace PithiaV2.Dtos.Course;

public class CourseReadDto
{
    public int Id { get; set; }

    public string? CourseCharacteristic { get; set; }

    public string? CourseName { get; set; }

    public int Specialization { get; set; }

    public int TheoryHours { get; set; }
    
    public int LabHours { get; set; }
    
    
}