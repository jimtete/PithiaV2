using AutoMapper;
using PithiaV2.Dtos.StudentXCourse;
using PithiaV2.Models;

namespace PithiaV2.Profiles;

public class StudentXCourseProfile : Profile
{

    public StudentXCourseProfile()
    {
        CreateMap<StudentXCourse, StudentXCourseReadDto>();
        CreateMap<StudentXCourseUpdateDto, StudentXCourse>();
    }
    
}