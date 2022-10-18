using AutoMapper;
using PithiaV2.Dtos.Professor;
using PithiaV2.Models;

namespace PithiaV2.Profiles;

public class ProfessorProfile : Profile
{

    public ProfessorProfile()
    {

        CreateMap<Professor, ProfessorReadDto>();
        CreateMap<ProfessorCreateDto,Professor>();
        CreateMap<ProfessorUpdateDto, Professor>();

    }
    
}