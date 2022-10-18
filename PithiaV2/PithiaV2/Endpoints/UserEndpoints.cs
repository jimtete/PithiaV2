using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PithiaV2.Data;
using PithiaV2.Dtos.StudentXCourse;
using PithiaV2.Dtos.User;
using PithiaV2.EndpointManager;
using PithiaV2.Models;

namespace PithiaV2.Endpoints;

public class UserEndpoints : IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IUserRepo, UserRepo>();
    }

    internal async Task<IResult> GetUserById(IUserRepo repo, IMapper mapper, int uid)
    {
        var user = await repo.GetUserById(uid);
        if (user == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(mapper.Map<UserReadDto>(user));
    }

    internal async Task<IResult> GetAllUsers(IUserRepo repo, IMapper mapper)
    {
        var Users = await repo.GetAllUsers();
        return Results.Ok(mapper.Map<IEnumerable<UserReadDto>>(Users));
    }

    internal async Task<IResult> CreateUser(IUserRepo repo, IMapper mapper, UserCreateDto user)
    {
        var userModel = mapper.Map<User>(user);
        await repo.CreateUser(userModel);
        await repo.SaveChange();

        var userReadDto = mapper.Map<UserReadDto>(userModel);

        return Results.Created($"/users/{userReadDto.Id}", userReadDto);
    }

    internal async Task<IResult> UpdateUserById(IMapper mapper, IUserRepo repo, UserUpdateDto userUpdateDto, int uid)
    {
        var user = await repo.GetUserById(uid);
        if (user == null)
        {
            return Results.NotFound();
        }

        if (userUpdateDto.SchoolCharacteristic != null)
        {
            user.SchoolCharacteristic = userUpdateDto.SchoolCharacteristic;
        }

        if (userUpdateDto.age != null)
        {
            user.age = userUpdateDto.age;
        }

        if (userUpdateDto.FirstName != null)
        {
            user.FirstName = userUpdateDto.FirstName;
        }

        if (userUpdateDto.LastName != null)
        {
            user.LastName = userUpdateDto.LastName;
        }

        if (userUpdateDto.BirthYear != null)
        {
            user.BirthYear = userUpdateDto.BirthYear;
        }
                
        var result = mapper.Map<UserReadDto>(user);

        await repo.SaveChange();
        return Results.Ok(result);
    }

    internal async Task<IResult> DeleteUserById(IUserRepo repo, int uid)
    {
        var user = await repo.GetUserById(uid);
        if (user == null)
        {
            return Results.NotFound();
        }
                
        repo.DeleteUser(user);
        await repo.SaveChange();

        return Results.NoContent();
    }

    internal async Task<IResult> ChangeGrade([FromBody] float grade, IMapper mapper, IUserRepo repo, int uid)
    {
        var user = await repo.GetUserById(uid);
        if (user == null)
        {
            return Results.NotFound();
        }

        user.Grade = (float)Math.Round(grade,2);

        await repo.SaveChange();
        var result = mapper.Map<UserReadDto>(user);
        
        return  Results.Ok(result);

    }
    
    public void DefineEndpoints(WebApplication app)
    {
        //Get
        app.MapGet("users", GetAllUsers);
        app.MapGet("users/{uid}", GetUserById);
        app.MapPost("users", CreateUser);
        app.MapPut("/users/{uid}", UpdateUserById);
        app.MapDelete("users/{uid}", DeleteUserById);
        app.MapPut("/users/setgrade/{uid}", ChangeGrade);
    }

}