namespace PithiaV2.Dtos.PassingGrade;

public class PassingGradeReadDto
{
    public int Id { get; set; }

    public int GradingBookletId { get; set; }

    public int StudentXCourseId { get; set; }

    public int ProfessorId { get; set; }

    public float Grade { get; set; }

}