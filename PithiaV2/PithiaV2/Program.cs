using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PithiaV2.Data;
using PithiaV2.Dtos.StudentXCourse;
using PithiaV2.Endpoints;
using PithiaV2.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ICourseRepo, CourseRepo>();
builder.Services.AddScoped<IStudentXCourseRepo, StudentXCourseRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.Add(UserEndpoints);

var app = builder.Build();

app.UsersEndpoints();
app.CoursesEndpoints();
app.ParticipationEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();