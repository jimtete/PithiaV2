using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using PithiaV2.Data;
using PithiaV2.Dtos.GradingBooklet;
using PithiaV2.EndpointManager;
using PithiaV2.Models;

namespace PithiaV2.Endpoints;

public class GradingBookletEndpoints : IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IGradingBookletRepo, GradingBookletRepo>();
    }

    internal async Task<IResult> CreateNewBooklet(GradingBookletCreateDto gradingBookletCreateDto,
        IMapper mapper, IGradingBookletRepo repo, IUserRepo userRepo)
    {
        var ValidationResults = gradingBookletCreateDto.Validate(new ValidationContext(gradingBookletCreateDto))
            .Select(gb => gb.ErrorMessage)
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

        if (await repo.GetBookletByUserId(gradingBookletCreateDto.UserId) != null)
        {
            return Results.BadRequest("There is already a booklet owned by this user");
        }
        
        var user = await userRepo.GetUserById(gradingBookletCreateDto.UserId);
        if (user == null)
        {
            return Results.BadRequest("User not found");
        }

        var gradingBooklet = mapper.Map<GradingBooklet>(gradingBookletCreateDto);
        gradingBooklet.User = user;
        gradingBooklet.GradingSum = 0;
        gradingBooklet.PassedCourses = 0;

        await repo.CreateBooklet(gradingBooklet);
        await repo.SaveChanges();

        var results = mapper.Map<GradingBookletReadDto>(gradingBooklet);
        return Results.Created($"/gbooklet/{results.Id}", results);



    }

    internal async Task<IResult> GetAllBooklets(IMapper mapper, IGradingBookletRepo repo)
    {
        var booklets = await repo.GetAllBooklets();
        return Results.Ok(mapper.Map<List<GradingBookletReadDto>>(booklets));
    }

    internal async Task<IResult> GetBookletById(int gbid, IMapper mapper, IGradingBookletRepo repo)
    {
        var booklet = await repo.GetBookletById(gbid);
        if (booklet == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(mapper.Map<GradingBookletReadDto>(booklet));
    }

    internal async Task<IResult> DeleteBookletById(int gbid, IGradingBookletRepo repo)
    {
        var booklet = await repo.GetBookletById(gbid);
        if (booklet == null)
        {
            return Results.NotFound();
        }
        
        repo.DeleteBooklet(booklet);
        repo.SaveChanges();
        return Results.NoContent();

    }
    
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/gbooklet", GetAllBooklets);
        app.MapPost("/gbooklet", CreateNewBooklet);
        app.MapGet("/gbooklet/{gbid}",GetBookletById);
        app.MapDelete("/gbooklet/{gbid}", DeleteBookletById);
    }
}