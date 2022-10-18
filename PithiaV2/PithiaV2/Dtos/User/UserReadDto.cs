namespace PithiaV2.Dtos.User;

public class UserReadDto
{
    public int Id { get; set; }

    public string SchoolCharacteristic { get; set; }
    
    public int age { get; set; }
    
    public float Grade { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

}