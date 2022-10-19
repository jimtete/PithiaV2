using AutoMapper;
using PithiaV2.Dtos.Lesson;
using PithiaV2.Models;

namespace PithiaV2.Profiles;

public class LessonProfile : Profile
{

    public LessonProfile()
    {
        CreateMap<Lesson,LessonReadDto>();
        CreateMap<LessonCreateDto,Lesson>();
        CreateMap<LessonUpdateDto, Lesson>();
    }
    
}