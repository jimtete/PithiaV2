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
    
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/lessons", CreateLesson);
    }
}