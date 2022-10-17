using AutoMapper;
using PithiaV2.Data;
using PithiaV2.Dtos.Course;
using PithiaV2.EndpointManager;
using PithiaV2.Models;

namespace PithiaV2.Endpoints;

public class CourseEndpoints : IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<ICourseRepo, CourseRepo>();
    }

    internal async Task<IResult> GetAllCourses(IMapper mapper, ICourseRepo repo)
    {
        var Courses = await repo.GetAllCourses();
        return Results.Ok(mapper.Map<IEnumerable<CourseReadDto>>(Courses));
    }

    internal async Task<IResult> GetCourseById(IMapper mapper, ICourseRepo repo, int cid)
    {
        var course = await repo.GetCourseById(cid);
        if (course == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(mapper.Map<CourseReadDto>(course));
    }

    internal async Task<IResult> CreateCourse(CourseCreateDto course, IMapper mapper, ICourseRepo repo)
    {
        var courseModel = mapper.Map<Course>(course);
        await repo.CreateCourse(courseModel);
        await repo.SaveChanges();

        var courseResults = mapper.Map<CourseReadDto>(courseModel);

        return Results.Created($"/courses/{courseResults.Id}", courseResults);
    }

    internal async Task<IResult> UpdateCourseById(int cid, CourseUpdateDto course, ICourseRepo repo, IMapper mapper)
    {
        var courseModel = await repo.GetCourseById(cid);
        if (courseModel == null)
        {
            return Results.NotFound();
        }

        if (course.CourseCharacteristic != null)
        {
            courseModel.CourseCharacteristic = course.CourseCharacteristic;
        }

        if (course.TheoryHours != null)
        {
            courseModel.TheoryHours = course.TheoryHours;
        }

        if (course.LabHours != null)
        {
            courseModel.LabHours = course.LabHours;
        }
            
        var results = mapper.Map<CourseReadDto>(courseModel);
        await repo.SaveChanges();
        return Results.Ok(results);
    }

    internal async Task<IResult> DeleteCourseById(ICourseRepo repo, int cid)
    {
        var course = await repo.GetCourseById(cid);
        if (course == null)
        {
            return Results.NotFound();
        }
                
        repo.DeleteCourse(course);
        await repo.SaveChanges();

        return Results.NoContent();
    }
    
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("courses", GetAllCourses);
        app.MapGet("courses/{cid}", GetCourseById);
        app.MapPost("courses", CreateCourse);
        app.MapPut("courses/{cid}", UpdateCourseById);
        app.MapDelete("courses/{cid}", DeleteCourseById);
    }
}