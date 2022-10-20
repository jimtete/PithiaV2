using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Models;

public class PassingGrade
{
    [Key] public int Id { get; set; }

    public int GradingBookletId { get; set; }
    public GradingBooklet GradingBooklet { get; set; } 
    
    public int StudentXCourseId { get; set; }
    public StudentXCourse StudentXCourse { get; set; }

    public Professor Professor { get; set; }
    public int ProfessorId { get; set; }

    public float Grade { get; set; } //must be between 5 and 10;
}