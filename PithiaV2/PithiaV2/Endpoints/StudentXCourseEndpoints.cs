using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PithiaV2.Data;
using PithiaV2.Dtos.Course;
using PithiaV2.Dtos.StudentXCourse;
using PithiaV2.EndpointManager;
using PithiaV2.Models;

namespace PithiaV2.Endpoints;

public class StudentXCourseEndpoints : IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IStudentXCourseRepo, StudentXCourseRepo>();
    }

    internal async Task<IResult> GetParticipationsByUserId(IMapper mapper, IStudentXCourseRepo repo, int uid)
    {
        var Participations = await repo.GetStudiesByStudentId(uid);
        return Results.Ok(mapper.Map<IEnumerable<StudentXCourseReadDto>>(Participations));
    }

    internal async Task<IResult> GetParticipationsByCourseId(IMapper mapper, IStudentXCourseRepo repo, int cid)
    {
        var Participations = await repo.GetStudiesByCourseId(cid);
        return Results.Ok(mapper.Map<IEnumerable<StudentXCourseReadDto>>(Participations));
    }

    internal async Task<IResult> GetAllParticipations(IMapper mapper, IStudentXCourseRepo repo)
    {
        var Participations = await repo.GetAllStudies();
        return Results.Ok(mapper.Map<IEnumerable<StudentXCourseReadDto>>(Participations));

    }

    internal async Task<IResult> GetParticipationsByUserCourseId(int cid, int uid, IMapper mapper, IStudentXCourseRepo repo)
    {
        var Participations = await repo.GetStudiesByCourseAndStudentId(uid, cid);
        return Results.Ok(mapper.Map<IEnumerable<StudentXCourseReadDto>>(Participations));
    }

    internal async Task<IResult> GetParticipationById(int id, IMapper mapper, IStudentXCourseRepo repo)
    {
        var participation = await repo.GetStudiesById(id);
        if (participation == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(mapper.Map<StudentXCourseReadDto>(participation));

    }

    internal async Task<IResult> CreateParticipation(int uid, int cid, IUserRepo userRepo, ICourseRepo courseRepo,
        IStudentXCourseRepo repo)
    {
        var participation = new StudentXCourse();
        var user = await userRepo.GetUserById(uid);
        if (user == null)
        {
            return Results.NotFound("User not found");
        }

        var course = await courseRepo.GetCourseById(cid);
        if (course == null)
        {
            return Results.NotFound("Course not found");
        }

        participation.Course = course;
        participation.CourseId = course.Id;
        participation.User = user;
        participation.UserId = user.Id;

        await repo.CreateStudies(participation);
        await repo.SaveChanges();

        Console.WriteLine(course.StudentXCourses.Count);
        return Results.Ok("Participation Created");

    }

    internal async Task<IResult> GetMultipleParticipationsById([FromBody] int [] arr, IMapper mapper, IStudentXCourseRepo repo)
    {
        var Participations = new List<StudentXCourse>();

        foreach (int id in arr)
        {
            var participation = await repo.GetStudiesById(id);
            if (participation != null)
            {
                Participations.Add(participation);
            }
        }
        
        return Results.Ok(mapper.Map<IEnumerable<StudentXCourseReadDto>>(Participations));
    }
    
    
    
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("participations", GetAllParticipations);
        app.MapGet("participations/userid/{uid}", GetParticipationsByUserId);
        app.MapGet("participations/courseid/{cid}",GetParticipationsByCourseId);
        app.MapGet("participations/c/{cid}/u/{uid}",GetParticipationsByUserCourseId);
        app.MapGet("participations/{sxcid}", GetParticipationById);
        app.MapPost("participations/{uid}/{cid}", CreateParticipation);
        //Be trying something new
        app.MapGet("participations/s/", GetMultipleParticipationsById);

    }
}