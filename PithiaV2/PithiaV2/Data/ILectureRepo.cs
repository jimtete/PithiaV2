using PithiaV2.Models;

namespace PithiaV2.Data;

public interface ILectureRepo
{
    Task SaveChanges();

    Task<Lecture> GetLectureById(int id);
    Task<List<Lecture>> GetAllLectures();
    Task CreateLecture(Lecture lecture);

    void DeleteLecture(Lecture lecture);

    Task<List<Lecture>> GetLecturesByProfessorId(int pid);
    Task<List<Lecture>> GetLecturesByCourseId(int cid);
    Task<List<Lecture>> GetLecturesByProfessorCourseId(int pid, int cid);

}