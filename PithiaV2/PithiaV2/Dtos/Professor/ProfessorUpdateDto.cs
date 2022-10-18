using System.ComponentModel.DataAnnotations;

namespace PithiaV2.Dtos.Professor;

public class ProfessorUpdateDto
{
    [Range(0,4)]
    public int? Rank { get; set; }

    public float Salary { get; set; }
    
    
}