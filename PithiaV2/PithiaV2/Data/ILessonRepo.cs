using PithiaV2.Models;

namespace PithiaV2.Data;

public interface ILessonRepo
{

    Task SaveChanges();

    Task<Lesson> GetLessonById(int id);
    Task<List<Lesson>> GetAllLessons();
    Task CreateLesson(Lesson lesson);
    
    void DeleteLesson(Lesson lesson);

}