using Microsoft.EntityFrameworkCore;
using PithiaV2.Models;

namespace PithiaV2.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<StudentXCourse> StudentXCourses => Set<StudentXCourse>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Course> Courses => Set<Course>();

}