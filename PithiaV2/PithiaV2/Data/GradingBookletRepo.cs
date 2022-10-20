using Microsoft.EntityFrameworkCore;
using PithiaV2.Models;

namespace PithiaV2.Data;

public class GradingBookletRepo : IGradingBookletRepo
{
    private readonly AppDbContext _context;

    public GradingBookletRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<GradingBooklet> GetBookletById(int id)
    {
        return await _context.GradingBooklets.FirstOrDefaultAsync(gb => gb.Id == id);
    }

    public async Task<List<GradingBooklet>> GetAllBooklets()
    {
        return await _context.GradingBooklets.ToListAsync();
    }

    public async Task CreateBooklet(GradingBooklet gradingBooklet)
    {
        if (gradingBooklet == null)
        {
            throw new ArgumentNullException();
        }

        await _context.GradingBooklets.AddAsync(gradingBooklet);
    }

    public void DeleteBooklet(GradingBooklet gradingBooklet)
    {
        if (gradingBooklet == null)
        {
            throw new ArgumentNullException();
        }

        _context.Remove(gradingBooklet);
    }

    public async Task<GradingBooklet> GetBookletByUserId(int uid)
    {
        return await _context.GradingBooklets.FirstOrDefaultAsync(gb => gb.UserId == uid);
    }
}