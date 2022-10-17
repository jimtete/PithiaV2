using AutoMapper;
using PithiaV2.Data;
using PithiaV2.Dtos.Course;
using PithiaV2.Models;

namespace PithiaV2.Endpoints;

public static class CourseEndpoints
{
    public static void CoursesEndpoints(this WebApplication app)
    {
        app.MapGet("courses", async (IMapper mapper, ICourseRepo repo) =>
        {
            var Courses = await repo.GetAllCourses();
            return Results.Ok(mapper.Map<IEnumerable<CourseReadDto>>(Courses));
        });

        app.MapGet("courses/{cid}", async (int cid, IMapper mapper, ICourseRepo repo) =>
        {
            var course = await repo.GetCourseById(cid);
            if (course == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(mapper.Map<CourseReadDto>(course));
        });

        app.MapPost("courses", async (CourseCreateDto course, IMapper mapper, ICourseRepo repo) =>
        {
            var courseModel = mapper.Map<Course>(course);
            await repo.CreateCourse(courseModel);
            await repo.SaveChanges();

            var courseResults = mapper.Map<CourseReadDto>(courseModel);

            return Results.Created($"/courses/{courseResults.Id}", courseResults);
        });

        app.MapPut("courses/{cid}", async (
            int cid, CourseUpdateDto course, ICourseRepo repo, IMapper mapper) =>
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
        });
        app.MapDelete("courses/{cid}",
            async (ICourseRepo repo, IMapper mapper, int cid) =>
            {
                var course = await repo.GetCourseById(cid);
                if (course == null)
                {
                    return Results.NotFound();
                }
                
                repo.DeleteCourse(course);
                await repo.SaveChanges();

                return Results.NoContent();

            });
        
    }
}