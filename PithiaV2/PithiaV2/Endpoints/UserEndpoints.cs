using AutoMapper;
using PithiaV2.Data;
using PithiaV2.Dtos.StudentXCourse;
using PithiaV2.Dtos.User;
using PithiaV2.Models;

namespace PithiaV2.Endpoints;

public static class UserEndpoints
{
    public static void UsersEndpoints(this WebApplication app)
    {
        //Get
        app.MapGet("users", async (IUserRepo repo, IMapper mapper) =>
        {
            var Users = await repo.GetAllUsers();
            return Results.Ok(mapper.Map<IEnumerable<UserReadDto>>(Users));
        });

        app.MapGet("users/{uid}", async (int uid, IUserRepo repo, IMapper mapper) =>
        {
            var user = await repo.GetUserById(uid);
            if (user == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(mapper.Map<UserReadDto>(user));

        });

        app.MapPost("users", async (UserCreateDto user, IUserRepo repo, IMapper mapper) =>
        {
            var userModel = mapper.Map<User>(user);
            await repo.CreateUser(userModel);
            await repo.SaveChange();

            var userReadDto = mapper.Map<UserReadDto>(userModel);

            return Results.Created($"/users/{userReadDto.Id}", userReadDto);

        });

        app.MapPut("/users/{uid}",
            async (IMapper mapper, IUserRepo repo, int uid, UserUpdateDto userUpdateDto) =>
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

            });

        app.MapDelete("users/{uid}",
            async (IUserRepo repo, IMapper mapper, int uid) =>
            {
                var user = await repo.GetUserById(uid);
                if (user == null)
                {
                    return Results.NotFound();
                }
                
                repo.DeleteUser(user);
                await repo.SaveChange();

                return Results.NoContent();

            });
    }
    
    
    
    
}