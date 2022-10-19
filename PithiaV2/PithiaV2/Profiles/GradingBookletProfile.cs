using AutoMapper;
using PithiaV2.Dtos.GradingBooklet;
using PithiaV2.Models;

namespace PithiaV2.Profiles;

public class GradingBookletProfile : Profile
{

    public GradingBookletProfile()
    {
        CreateMap<GradingBooklet, GradingBookletReadDto>();
        CreateMap<GradingBookletCreateDto, GradingBooklet>();
        CreateMap<GradingBookletUpdateDto, GradingBooklet>();
    }
    
}