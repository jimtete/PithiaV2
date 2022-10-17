using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.StudentXCourse;

public class SudentXCourseCreateDto
{
    [Required] public Models.User User { get; set; }
    [Required] public Models.Course Course { get; set; }
}