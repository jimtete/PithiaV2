using PithiaV2.Models;

namespace PithiaV2.Data;

public interface IPassingGradesRepo
{
    Task SaveChanges();

    Task<PassingGrade> GetPassingGradeById(int id);

    Task<List<PassingGrade>> GetAllPassingGrades();
    Task<List<PassingGrade>> GetAllPassingGradesByBookletId(int gbid);
    Task<List<PassingGrade>> GetAllPassingGradesByProfessorId(int pid);

    Task CreatePassingGrade(PassingGrade passingGrade);

    void DeletePassingGrade(PassingGrade passingGrade);

}