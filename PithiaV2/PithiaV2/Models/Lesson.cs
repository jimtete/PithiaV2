using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Models;

public class Lesson
{
    
    [Key]
    public int Id { get; set; }
    
    [Required] public Lecture Lecture { get; set; }
    [Required] public int LectureId { get; set; }
    
    //
    [Required]
    public string? Day { get; set; } // Monday, Tuesday, Wednesday, Thursday, Friday, Saturday

    public int Hours { get; set; }
    public int Minutes { get; set; }
    
    //
    [Required] public string? Time { get; set; } //needs validation
    
    
}