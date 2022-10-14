using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.User;

public class UserCreateDto
{

    [Required]
    [MaxLength(5)]
    public string SchoolCharacteristic { get; set; }

    public int age { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public int BirthYear { get; set; }
}