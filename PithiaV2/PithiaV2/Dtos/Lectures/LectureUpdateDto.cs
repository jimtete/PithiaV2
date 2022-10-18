using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.Lectures;

public class LectureUpdateDto
{
    
    [Required] public Models.Professor Professor { get; set; }
    [Required] public Models.Course Course { get; set; }
    
}