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