using PithiaV2.Models;

namespace PithiaV2.Data;

public interface IStudentXCourseRepo
{
    Task SaveChanges();

    Task<StudentXCourse> GetStudiesById(int id);
    Task<IEnumerable<StudentXCourse>> GetAllStudies();
    Task<IEnumerable<StudentXCourse>> GetStudiesByStudentId(int uid);
    Task<IEnumerable<StudentXCourse>> GetStudiesByCourseId(int cid);
    Task<IEnumerable<StudentXCourse>> GetStudiesByCourseAndStudentId(int uid, int cid);
    Task CreateStudies(StudentXCourse studies);

    void DeleteStudies(StudentXCourse studies);


}