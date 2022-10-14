using PithiaV2.Models;

namespace PithiaV2.Data;

public interface ICourseRepo
{
    Task SaveChanges();

    Task<Course> GetCourseById(int id);
    Task<IEnumerable<Course>> GetAllCourses();
    Task CreateCourse(Course course);

    void DeleteCourse(Course course);


}