using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PithiaV2.Dtos.User;

public class UserUpdateDto
{
    [Required]
    [MaxLength(5)]
    public string SchoolCharacteristic { get; set; }
    
    public int age { get; set; }
    
    public float Grade { get; set; }
    
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }

    [Required]
    public int BirthYear { get; set; }
}