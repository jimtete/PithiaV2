using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using PithiaV2.Data;
using PithiaV2.Dtos.PassingGrade;
using PithiaV2.EndpointManager;
using PithiaV2.Models;

namespace PithiaV2.Endpoints;

public class PassingGradeEndpoints : IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IPassingGradesRepo, PassingGradesRepo>();
    }

    internal async Task<IResult> GetAllGrades(IPassingGradesRepo repo, IMapper mapper)
    {
        var result = await repo.GetAllPassingGrades();
        return Results.Ok(mapper.Map<List<PassingGradeReadDto>>(result));
    }
    
    internal async Task<IResult> AddPassingGrade(IPassingGradesRepo repo, IProfessorRepo professorRepo, 
        IStudentXCourseRepo studentXCourseRepo, IGradingBookletRepo gradingBookletRepo, IMapper mapper,
        PassingGradeCreateDto passingGradeCreateDto, IUserRepo userRepo)
    {
        var ValidationResults = passingGradeCreateDto.Validate(new ValidationContext(passingGradeCreateDto))
            .Select(pg => pg.ErrorMessage)
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

        var professor = await professorRepo.GetProfessorById(passingGradeCreateDto.ProfessorId);
        if (professor == null)
        {
            return Results.BadRequest("Professor not found");
        }

        var participation = await studentXCourseRepo.GetStudiesById(passingGradeCreateDto.StudentXCourseId);
        if (participation == null)
        {
            return Results.BadRequest("Participation not found");
        }

        var booklet = await gradingBookletRepo.GetBookletById(passingGradeCreateDto.GradingBookletId);
        if (booklet == null)
        {
            return Results.BadRequest("Booklet not found");
        }

        
        
        

        var passingGrade = mapper.Map<PassingGrade>(passingGradeCreateDto);
        passingGrade.Professor = professor;
        passingGrade.GradingBooklet = booklet;
        passingGrade.StudentXCourse = participation;

        await repo.CreatePassingGrade(passingGrade);
        await repo.SaveChanges();
        
        booklet.GradingSum += passingGradeCreateDto.Grade;
        booklet.PassedCourses += 1;
        await gradingBookletRepo.SaveChanges();

        var userModel = await userRepo.GetUserById(passingGrade.GradingBooklet.UserId);
        userModel.Grade = ((booklet.GradingSum) / (booklet.PassedCourses));
        await userRepo.SaveChange();

        var results = mapper.Map<PassingGradeReadDto>(passingGrade);
        return Results.Created($"/grades/{results.Id}", results);
    }

    internal async Task<IResult> GetGradeById(int pgid, IPassingGradesRepo repo, IMapper mapper)
    {
        var grade = await repo.GetPassingGradeById(pgid);
        if (grade == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(mapper.Map<PassingGradeReadDto>(grade));
    }

    internal async Task<IResult> GetGradesByBookletId(int gbid, IPassingGradesRepo repo, IMapper mapper)
    {
        var grades = await repo.GetAllPassingGradesByBookletId(gbid);
        return Results.Ok(mapper.Map<List<PassingGradeReadDto>>(grades));
    }

    internal async Task<IResult> GetGradesByProfessorId(int pid, IPassingGradesRepo repo, IMapper mapper)
    {
        var grades = await repo.GetAllPassingGradesByProfessorId(pid);
        return Results.Ok(mapper.Map<List<PassingGradeReadDto>>(grades));
    }

    internal async Task<IResult> DeleteGradeById(int pgid, IPassingGradesRepo repo)
    {
        var grade = await repo.GetPassingGradeById(pgid);
        if (grade == null)
        {
            return Results.NotFound();
        }
        repo.DeletePassingGrade(grade);
        await repo.SaveChanges();
        
        return Results.NoContent();
    }
    
    
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/grades", AddPassingGrade);
        app.MapGet("/grades", GetAllGrades);
        app.MapGet("/grades/{pgid}", GetGradeById);
        app.MapGet("/grades/booklet/{gbid}", GetGradesByBookletId);
        app.MapGet("/grades/professor/{pid}", GetGradesByProfessorId);
        app.MapDelete("/grades/{pgid}", DeleteGradeById);
    }
}