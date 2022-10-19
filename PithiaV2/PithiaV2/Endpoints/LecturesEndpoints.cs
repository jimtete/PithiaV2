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

    internal async Task<IResult> GetLectureByProfessorId(int pid, IMapper mapper, ILectureRepo repo)
    {
        var Lectures = await repo.GetLecturesByProfessorId(pid);
        return Results.Ok(mapper.Map<List<Lecture>>(Lectures));
    }

    internal async Task<IResult> GetLectureByCourseId(int cid, IMapper mapper, ILectureRepo repo)
    {
        var Lectures = await repo.GetLecturesByCourseId(cid);
        return Results.Ok(mapper.Map<List<Lecture>>(Lectures));
    }

    internal async Task<IResult> GetLectureByProfessorCourseId(int cid, int pid, 
        IMapper mapper, ILectureRepo repo)
    {
        var Lectures = await repo.GetLecturesByProfessorCourseId(pid, cid);
        return Results.Ok(mapper.Map<List<Lecture>>(Lectures));
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
        
        return Results.Ok("Lecture created");
    }

    internal async Task<IResult> DeleteLectureById(ILectureRepo repo, int lcid)
    {
        var lecture = await repo.GetLectureById(lcid);
        if (lecture == null)
        {
            return Results.NotFound();
        }
        repo.DeleteLecture(lecture);
        repo.SaveChanges();

        return Results.NoContent();
    }
    
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/lectures/pid/{pid}", GetLectureByProfessorId);
        app.MapGet("lectures/cid/{cid}", GetLectureByCourseId);
        app.MapGet("/lectures/c/{cid}/p/{pid}", GetLectureByProfessorCourseId);
        app.MapGet("/lectures/p/{pid}/c/{cid}", GetLectureByProfessorCourseId);
        app.MapGet("/lectures/{lcid}", GetLectureById);
        app.MapPost("lectures", CreateNewLecture);
        app.MapDelete("/lectures/{lcid}", DeleteLectureById);
    }
}