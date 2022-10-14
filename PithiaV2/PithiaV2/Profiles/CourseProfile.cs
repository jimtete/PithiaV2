using AutoMapper;
using PithiaV2.Dtos.Course;
using PithiaV2.Models;

namespace PithiaV2.Profiles;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseReadDto>();
        CreateMap<CourseUpdateDto, Course>();
        CreateMap<CourseCreateDto, Course>();
    }
    
    
}