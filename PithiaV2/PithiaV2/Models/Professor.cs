using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Models;

public class Professor
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(64)]
    public string? FirstName { get; set; }
    [Required]
    [MaxLength(64)]
    public string? LastName { get; set; }
    [Range(0,4)]
    [Required]
    public int? Rank { get; set; }//0: Visiting professor, 1: Assistant professor, 2: Associate professor, 3: Professor, 4: Managing Professor
    [Required]
    public float Salary { get; set; }
    
    //To be added list :)
}