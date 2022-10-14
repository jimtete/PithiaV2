using AutoMapper;
using PithiaV2.Dtos.User;
using PithiaV2.Models;

namespace PithiaV2.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserReadDto>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<UserCreateDto, User>();
    } 
}