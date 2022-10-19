using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.Lectures;

public class LectureCreateDto
{
    [Required] public int ProfessorId { get; set; }
    [Required] public int CourseId { get; set; }
}