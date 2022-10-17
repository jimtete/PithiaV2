using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(5)]
    public string SchoolCharacteristic { get; set; }
    
    
    public int age { get; set; }
    
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }

    [Required]
    public int BirthYear { get; set; }

    public List<StudentXCourse>? StudentXCourses { get; set; }
    

}