namespace PithiaV2.Dtos.GradingBooklet;

public class GradingBookletReadDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public float GradingSum { get; set; }
    public int PassedCourses { get; set; }
    
    
}