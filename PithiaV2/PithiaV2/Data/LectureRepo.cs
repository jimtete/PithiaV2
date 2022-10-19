using Microsoft.EntityFrameworkCore;
using PithiaV2.Models;

namespace PithiaV2.Data;

public class LectureRepo : ILectureRepo
{
    private readonly AppDbContext _context;

    public LectureRepo(AppDbContext context)
    {
        _context = context;
    }
    
    
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Lecture> GetLectureById(int id)
    {
        return await _context.Lectures.FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<List<Lecture>> GetAllLectures()
    {
        return await _context.Lectures.ToListAsync();
    }

    public async Task<List<Lecture>> GetLecturesByProfessorId(int pid)
    {
        var TempList = await _context.Lectures.ToListAsync();
        foreach (var lecture in TempList)
        {
            if (lecture.ProfessorId != pid)
            {
                TempList.Remove(lecture);
            }
        }

        if (TempList.Count == 0)
        {
            throw new ArgumentNullException();
        }

        return TempList;
    }

    public async Task<List<Lecture>> GetLecturesByCourseId(int cid)
    {
        var TempList = await _context.Lectures.ToListAsync();
        foreach (var lecture in TempList)
        {
            if (lecture.CourseId != cid)
            {
                TempList.Remove(lecture);
            }
        }

        if (TempList.Count == 0)
        {
            throw new ArgumentNullException();
        }

        return TempList;
    }

    public async Task<List<Lecture>> GetLecturesByProfessorCourseId(int pid, int cid)
    {
        var FinalList = await _context.Lectures.ToListAsync();
        foreach (var lecture in FinalList)
        {
            if (lecture.CourseId != cid || lecture.ProfessorId != pid)
            {
                FinalList.Remove(lecture);
            }
        }

        if (FinalList.Count == 0)
        {
            throw new ArgumentNullException();
        }

        return FinalList;

    }


    public async Task CreateLecture(Lecture lecture)
    {
        if (lecture == null)
        {
            throw new ArgumentNullException();
        }

        await _context.Lectures.AddAsync(lecture);
    }

    public void DeleteLecture(Lecture lecture)
    {
        if (lecture == null)
        {
            throw new ArgumentNullException();
        }

        _context.Remove(lecture);
    }
}