using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Models;

public class Course
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(5)]
    public string? CourseCharacteristic { get; set; }
    
    [Required]
    public string? CourseName { get; set; }
    
    [Required]
    public int Specialization { get; set; } //0: Main, 1: Software Engineering, 2: Artificial Intelligence, 3: Networking
    
    [Required]
    public int TheoryHours { get; set; }
    
    public int LabHours { get; set; }

    public List<StudentXCourse>? StudentXCourses { get; set; }
    
}