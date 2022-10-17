using PithiaV2.Models;

namespace PithiaV2.Data;

public interface IStudentXCourseRepo
{
    Task SaveChanges();

    Task<StudentXCourse> GetStudiesById(int id);
    Task<IEnumerable<StudentXCourse>> GetAllStudies();
    Task CreateStudies(StudentXCourse studies);

    void DeleteStudies(StudentXCourse studies);


}