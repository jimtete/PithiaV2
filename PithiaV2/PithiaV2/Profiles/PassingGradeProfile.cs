using AutoMapper;
using PithiaV2.Data;
using PithiaV2.Dtos.PassingGrade;
using PithiaV2.Models;

namespace PithiaV2.Profiles;

public class PassingGradeProfile : Profile
{

    public PassingGradeProfile()
    {
        CreateMap<PassingGrade,PassingGradeReadDto>();
        CreateMap<PassingGradeCreateDto, PassingGrade>();
    }
    
}