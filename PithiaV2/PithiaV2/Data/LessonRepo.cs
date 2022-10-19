using Microsoft.EntityFrameworkCore;
using PithiaV2.Models;

namespace PithiaV2.Data;

public class LessonRepo : ILessonRepo
{

    private readonly AppDbContext _context;

    public LessonRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Lesson> GetLessonById(int id)
    {
        return await _context.Lessons.FirstOrDefaultAsync(lss => lss.Id == id);
    }

    public async Task<List<Lesson>> GetAllLessons()
    {
        return await _context.Lessons.ToListAsync();
    }

    public async Task CreateLesson(Lesson lesson)
    {
        if (lesson == null)
        {
            throw new ArgumentNullException();
        }

        await _context.AddAsync(lesson);
    }

    public void DeleteLesson(Lesson lesson)
    {
        if (lesson == null)
        {
            throw new ArgumentNullException();
        }

        _context.Remove(lesson);
    }
}