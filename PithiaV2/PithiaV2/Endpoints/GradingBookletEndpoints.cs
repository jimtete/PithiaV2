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
        
        
        
        var user = await userRepo.GetUserById(gradingBookletCreateDto.UserId);
        if (user == null)
        {
            return Results.BadRequest("User not found");
        }

        var gradingBooklet = mapper.Map<GradingBooklet>(gradingBookletCreateDto);
        gradingBooklet.User = user;

        await repo.CreateBooklet(gradingBooklet);
        await repo.SaveChanges();

        var results = mapper.Map<GradingBookletReadDto>(gradingBooklet);
        return Results.Created($"/gbooklet/{results.Id}", results);



    }
    
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/gbooklet", CreateNewBooklet);
    }
}