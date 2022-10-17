using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.StudentXCourse;

public class SudentXCourseCreateDto
{
    [Required] public int StudentId { get; set; }
    [Required] public int CourseId { get; set; }
}