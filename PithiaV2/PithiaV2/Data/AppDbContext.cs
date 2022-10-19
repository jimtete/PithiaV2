using Microsoft.EntityFrameworkCore;
using PithiaV2.Models;

namespace PithiaV2.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }


    public DbSet<GradingBooklet> GradingBooklets => Set<GradingBooklet>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Lecture> Lectures => Set<Lecture>();
    public DbSet<Professor> Professors => Set<Professor>();
    public DbSet<StudentXCourse> StudentXCourses => Set<StudentXCourse>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Course> Courses => Set<Course>();

}