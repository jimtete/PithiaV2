namespace PithiaV2.Dtos.StudentXCourse;

public class StudentXCourseReadDto
{
    public int Id { get; set; }

    public Models.User User { get; set; }
    public Models.Course Course { get; set; }
    
    
}