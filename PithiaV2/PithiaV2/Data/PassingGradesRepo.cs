using Microsoft.EntityFrameworkCore;
using PithiaV2.Models;

namespace PithiaV2.Data;

public class PassingGradesRepo : IPassingGradesRepo
{

    private readonly AppDbContext _context;

    public PassingGradesRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task CreatePassingGrade(PassingGrade passingGrade)
    {
        if (passingGrade == null)
        {
            throw new ArgumentNullException();
        }

        await _context.AddAsync(passingGrade);
    }

    public async Task<PassingGrade> GetPassingGradeById(int id)
    {
        return await _context.PassingGrades.FirstOrDefaultAsync(pg => pg.Id == id);
    }

    public async Task<List<PassingGrade>> GetAllPassingGrades()
    {
        return await _context.PassingGrades.ToListAsync();
    }

    public async Task<List<PassingGrade>> GetAllPassingGradesByBookletId(int gbid)
    {
        var TempList = await _context.PassingGrades.ToListAsync();
        foreach (var pass in TempList)
        {
            if (pass.GradingBookletId != gbid)
            {
                TempList.Remove(pass);
            }
        }

        return TempList;

    }

    public async Task<List<PassingGrade>> GetAllPassingGradesByProfessorId(int pid)
    {
        var TempList = await _context.PassingGrades.ToListAsync();
        foreach (var pass in TempList)
        {
            if (pass.ProfessorId != pid)
            {
                TempList.Remove(pass);
            }
        }

        return TempList;
    }

    public void DeletePassingGrade(PassingGrade passingGrade)
    {
        if (passingGrade == null)
        {
            throw new ArgumentNullException();
        }

        _context.Remove(passingGrade);
    }
}