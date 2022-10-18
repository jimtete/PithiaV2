using Microsoft.EntityFrameworkCore;
using PithiaV2.Models;

namespace PithiaV2.Data;

public class StudentXCourseRepo : IStudentXCourseRepo
{
    private readonly AppDbContext _context;

    public StudentXCourseRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudentXCourse>> GetStudiesByStudentId(int uid)
    {
        var TempList = await _context.StudentXCourses.ToListAsync();
        var FinalList = new List<StudentXCourse>();
        if (TempList.Count == 0)
        {
            throw new ArgumentNullException();
        }

        foreach (var part in TempList)
        {
            if (part.UserId == uid)
            {
                FinalList.Add(part);
            }
        }

        if (FinalList.Count == 0)
        {
            throw new ArgumentNullException();
        }

        return FinalList;
    }

    public async Task<IEnumerable<StudentXCourse>> GetStudiesByCourseId(int cid)
    {
        var TempList = await _context.StudentXCourses.ToListAsync();
        var FinalList = new List<StudentXCourse>();

        if (TempList.Count == 0)
        {
            throw new ArgumentNullException();
        }

        foreach (var part in TempList)
        {
            if (part.CourseId == cid)
            {
                FinalList.Add(part);
            }
        }

        if (FinalList.Count == 0)
        {
            throw new ArgumentNullException();
        }

        return FinalList;
    }

    public async Task<IEnumerable<StudentXCourse>> GetStudiesByCourseAndStudentId(int uid,int cid)
    {
        var DraftOne = await _context.StudentXCourses.ToListAsync();
        if (DraftOne.Count == 0)
        {
            throw new ArgumentNullException();
        }
        
        var DraftTwo = new List<StudentXCourse>();

        foreach (var part in DraftOne)
        {
            if (part.UserId == uid)
            {
                DraftTwo.Add(part);
            }
        }

        if (DraftTwo.Count == 0)
        {
            throw new ArgumentNullException();
        }

        var FinalDraft = new List<StudentXCourse>();

        foreach (var part in DraftTwo)
        {
            if (part.CourseId == cid)
            {
                FinalDraft.Add(part);
            }
        }

        if (FinalDraft.Count == 0)
        {
            throw new ArgumentNullException();
        }

        return FinalDraft;


    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<StudentXCourse> GetStudiesById(int id)
    {
        return await _context.StudentXCourses.FirstOrDefaultAsync(sc => sc.Id == id);
    }

    public async Task<IEnumerable<StudentXCourse>> GetAllStudies()
    {
        return await _context.StudentXCourses!.ToListAsync();
    }

    public async Task CreateStudies(StudentXCourse studies)
    {
        if (studies == null)
        {
            throw new ArgumentNullException(nameof(studies));
        }

        await _context.AddAsync(studies);
    }

    public void DeleteStudies(StudentXCourse studies)
    {
        if (studies == null)
        {
            throw new ArgumentNullException(nameof(studies));
        }

        _context.Remove(studies);
    }
}