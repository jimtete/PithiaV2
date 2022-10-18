using Microsoft.EntityFrameworkCore;
using PithiaV2.Models;

namespace PithiaV2.Data;

public class ProfessorRepo : IProfessorRepo
{
    private readonly AppDbContext _context;

    public ProfessorRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Professor> GetProfessorById(int id)
    {
        return await _context.Professors.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Professor>> GetAllProfessors()
    {
        return await _context.Professors.ToListAsync();
    }

    public async Task CreateProfessor(Professor professor)
    {
        if (professor == null)
        {
            throw new ArgumentNullException();
        }
        
        await _context.Professors.AddAsync(professor);
    }

    public void DeleteProfessor(Professor professor)
    {
        if (professor == null)
        {
            throw new ArgumentNullException();
        }

        _context.Remove(professor);
    }
}