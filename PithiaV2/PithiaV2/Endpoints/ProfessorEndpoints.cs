using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PithiaV2.Data;
using PithiaV2.Dtos.Professor;
using PithiaV2.EndpointManager;
using PithiaV2.Models;

namespace PithiaV2.Endpoints;

public class ProfessorEndpoints : IEndpointDefinition
{

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IProfessorRepo, ProfessorRepo>();
    }

    internal async Task<IResult> CreateProfessor(IMapper mapper, IProfessorRepo repo, ProfessorCreateDto professor)
    {

        var ValidationResults = professor.Validate(new ValidationContext(professor))
            .Select(vr => vr.ErrorMessage)
            .ToList();

        if (ValidationResults.Count != 0)
        {
            var builder = new StringBuilder();

            foreach (var vr in ValidationResults)
            {
                builder.Append(vr);
            }
            return Results.BadRequest(builder.ToString());
        }
        
        
        var professorModel = mapper.Map<Professor>(professor);

        await repo.CreateProfessor(professorModel);
        await repo.SaveChanges();
        
        /*if (professorModel.Rank < 0 || professorModel.Rank > 4)
        {
            return Results.Problem($"Professor Rank {professorModel.Rank}: invalid");
        }*/
        var professorReadDto = mapper.Map<ProfessorReadDto>(professorModel);
        
        return Results.Created($"/professors/{professorReadDto.Id}",professorReadDto);

    }

    internal async Task<IResult> GetProfessorById(int pid, IMapper mapper, IProfessorRepo repo)
    {
        var Professor = await repo.GetProfessorById(pid);
        if (Professor == null)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(mapper.Map<ProfessorReadDto>(Professor));

    }

    internal async Task<IResult> GetAllProfessors(IMapper mapper, IProfessorRepo repo)
    {
        var Professors = await repo.GetAllProfessors();
        return Results.Ok(mapper.Map<List<ProfessorReadDto>>(Professors));
    }

    internal async Task<IResult> ChangeProfessorSalary([FromBody] float salary, int pid, IMapper mapper,
        IProfessorRepo repo)
    {
        var professor = await repo.GetProfessorById(pid);

        if (professor == null)
        {
            return Results.NotFound();
        }
        
        professor.Salary = salary;
        await repo.SaveChanges();
        
        return Results.Ok($"Salary succesfully changed to: {professor.Salary}");

    }

    internal async Task<IResult> UpdateProfessor(int pid, IMapper mapper, IProfessorRepo repo, ProfessorUpdateDto professor)
    {
        var professorModel = await repo.GetProfessorById(pid);

        if (professorModel == null)
        {
            return Results.NotFound();
        }

        if (professor.Rank != null)
        {
            professorModel.Rank = professor.Rank;
        }

        if (professor.Salary != null)
        {
            professorModel.Salary = professor.Salary;
        }

        await repo.SaveChanges();
        
        var result = mapper.Map<ProfessorReadDto>(professorModel);
        return Results.Ok(result);

    }

    internal async Task<IResult> DeleteProfessorById(int pid, IProfessorRepo repo)
    {
        var professor = await repo.GetProfessorById(pid);
        if (professor == null)
        {
            return Results.NotFound();
        }
        
        repo.DeleteProfessor(professor);
        await repo.SaveChanges();
        
        return Results.NoContent();
    }


    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("professors", CreateProfessor);
        app.MapGet("professors/{pid}", GetProfessorById);
        app.MapGet("professors", GetAllProfessors);
        app.MapPut("professors/changesalary/{pid}", ChangeProfessorSalary);
        app.MapPut("professors/{pid}", UpdateProfessor);
        app.MapDelete("professors/{pid}", DeleteProfessorById);
    }
    
    
}