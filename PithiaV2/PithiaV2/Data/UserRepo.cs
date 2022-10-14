using PithiaV2.Models;
using Microsoft.EntityFrameworkCore;

namespace PithiaV2.Data;

public class UserRepo : IUserRepo
{
    private readonly AppDbContext _context;

    public UserRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveChange()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _context.Users!.ToListAsync();
    }

    public async Task CreateUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await _context.AddAsync(user);
    }

    public void DeleteUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _context.Remove(user);
    }
}