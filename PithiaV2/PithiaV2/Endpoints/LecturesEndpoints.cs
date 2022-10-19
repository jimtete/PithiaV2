using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PithiaV2.Data;
using PithiaV2.Dtos.Course;
using PithiaV2.Dtos.Lectures;
using PithiaV2.EndpointManager;
using PithiaV2.Models;

namespace PithiaV2.Endpoints;

public class LecturesEndpoints : IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<ILectureRepo, LectureRepo>();
    }

    
    
    internal async Task<IResult> GetLectureById(int lcid, IMapper mapper, ILectureRepo repo)
    {
        var Lecture = await repo.GetLectureById(lcid);
        if (Lecture == null)
        {
            return Results.NotFound();
        }

        var result = mapper.Map<LectureReadDto>(Lecture);
        return Results.Ok(result);

    }
    
    internal async Task<IResult> CreateNewLecture([FromBody]LectureCreateDto lecture, ILectureRepo repo,
        ICourseRepo courseRepo, IProfessorRepo professorRepo)
    {
        var Lecture = new Lecture();
        Console.WriteLine(lecture.ProfessorId);
        var ProfessorModel = await professorRepo.GetProfessorById(lecture.ProfessorId);
        if (ProfessorModel == null) return Results.NotFound("Professor not found");

        var CourseModel = await courseRepo.GetCourseById(lecture.CourseId);
        if (CourseModel == null) return Results.NotFound("Course not found");

        Lecture.Course = CourseModel;
        Lecture.CourseId = lecture.CourseId;
        Lecture.ProfessorId = lecture.ProfessorId;
        Lecture.Professor = ProfessorModel;

        await repo.CreateLecture(Lecture);
        await repo.SaveChanges();

        Console.WriteLine(Lecture.Course.CourseName);
        return Results.Ok("Lecture created");
    }
    
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/lectures/{lcid}", GetLectureById);
        app.MapPost("lectures", CreateNewLecture);
    }
}