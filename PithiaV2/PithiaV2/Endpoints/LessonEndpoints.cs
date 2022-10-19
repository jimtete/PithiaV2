using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using PithiaV2.Data;
using PithiaV2.Dtos.Lesson;
using PithiaV2.EndpointManager;
using PithiaV2.Models;

namespace PithiaV2.Endpoints;

public class LessonEndpoints : IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<ILessonRepo, LessonRepo>();
    }

    internal async Task<IResult> CreateLesson(IMapper mapper, ILessonRepo repo, ILectureRepo lectureRepo,
        LessonCreateDto lesson)
    {
        var ValidationResults = lesson.Validate(new ValidationContext(lesson))
            .Select(le => le.ErrorMessage)
            .ToList();

        if (ValidationResults.Count != 0)
        {
            var builder = new StringBuilder();
            foreach (var error in ValidationResults)
            {
                builder.Append(error);
            }

            return Results.BadRequest(builder.ToString());
        }
        
        
        
        var lecture = await lectureRepo.GetLectureById(lesson.LectureId);

        var lessonModel = mapper.Map<Lesson>(lesson);
        lessonModel.Lecture = lecture;

        await repo.CreateLesson(lessonModel);
        await repo.SaveChanges();

        var results = mapper.Map<LessonReadDto>(lessonModel);
        return Results.Created($"/lessons/{results.Id}",results);
    }

    internal async Task<IResult> GetAllLessons(ILessonRepo repo, IMapper mapper)
    {
        var lessons = await repo.GetAllLessons();
        return Results.Ok(mapper.Map<List<LessonReadDto>>(lessons));
    }

    internal async Task<IResult> GetLessonById(ILessonRepo repo, IMapper mapper, int lsid)
    {
        var Lesson = await repo.GetLessonById(lsid);
        if (Lesson == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(mapper.Map<LessonReadDto>(Lesson));
    }

    internal async Task<IResult> UpdateLesson(ILessonRepo repo, IMapper mapper, int lsid, LessonUpdateDto lesson)
    {
        var lessonModel = await repo.GetLessonById(lsid);
        if (lessonModel == null)
        {
            return Results.NotFound();
        }

        var ValidationResults = lesson.Validate(new ValidationContext(lesson))
            .Select(er => er.ErrorMessage)
            .ToList();

        if (ValidationResults.Count != 0)
        {
            var builder = new StringBuilder();
            foreach (var error in ValidationResults)
            {
                builder.Append(error);
                builder.Append("/n");
            }
        }

        if (lesson.Day != null)
        {
            lessonModel.Day = lesson.Day;
        }

        lessonModel.Hours = lesson.Hours;

        lessonModel.Minutes = lesson.Minutes;

        if (lesson.Time != null)
        {
            lessonModel.Time = lesson.Time;
        }

        await repo.SaveChanges();

        var result = mapper.Map<LessonReadDto>(lessonModel);
        return Results.Ok(result);

    }

    internal async Task<IResult> DeleteLesson(int lsid, ILessonRepo repo)
    {
        var Lesson = await repo.GetLessonById(lsid);
        if (Lesson == null)
        {
            return Results.NotFound();
        }
        
        repo.DeleteLesson(Lesson);
        return Results.NoContent();
    }
    
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/lessons", CreateLesson);
        app.MapGet("/lessons", GetAllLessons);
        app.MapGet("/lessons/{lsid}", GetLessonById);
        app.MapPut("/lessons/{lsid}", UpdateLesson);
        app.MapDelete("/lessons/{lsid}", DeleteLesson);
    }
}