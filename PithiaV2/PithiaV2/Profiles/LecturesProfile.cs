using AutoMapper;
using PithiaV2.Data;
using PithiaV2.Dtos.Lectures;
using PithiaV2.Models;

namespace PithiaV2.Profiles;

public class LecturesProfile : Profile
{

    public LecturesProfile()
    {
        CreateMap<Lecture, LectureReadDto>();
        CreateMap<LectureCreateDto, Lecture>();
        CreateMap<LectureUpdateDto, Lecture>();
    }
    
}