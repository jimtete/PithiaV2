using AutoMapper;
using PithiaV2.Data;
using PithiaV2.Dtos.Course;
using PithiaV2.Dtos.StudentXCourse;
using PithiaV2.Models;

namespace PithiaV2.Endpoints;

public static class StudentXCourseEndpoints
{
    public static void ParticipationEndpoints(this WebApplication app)
    {
        
        app.MapGet("participations", async (IMapper mapper, IStudentXCourseRepo repo) =>
        {
            var StudentXCourses = await repo.GetAllStudies();
            var result = mapper.Map<IEnumerable<StudentXCourseReadDto>>(StudentXCourses);
            
            return Results.Ok(result);

        });
        
        //Participation by participation id
        app.MapGet("participations/{sxcid}",
            async (int sxcid, IMapper Mapper, IStudentXCourseRepo repo) =>
            {
                var participation = await repo.GetStudiesById(sxcid);
                if (participation == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(Mapper.Map<StudentXCourseReadDto>(participation));

            });

        app.MapPost("participations/{uid}/{cid}", async (int uid,int cid,
             IUserRepo userRepo, ICourseRepo courseRepo, IStudentXCourseRepo repo) =>
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

        });

    }
}