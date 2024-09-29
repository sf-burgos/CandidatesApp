using AutoMapper;
using CandidatesApp.Application.Commands;
using CandidatesApp.Models;
namespace CandidatesApp.Application.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Candidate, CandidateDTO>();
            CreateMap<Experience, ExperienceDto>();
            CreateMap<CandidateDTO, Candidate>();
            CreateMap<ExperienceDto, Experience>();
            CreateMap<CreateCandidateCommand, Candidate>();
            CreateMap<UpdateCandidateCommand, Candidate>();
            CreateMap<UpdateExperienceCommand, Experience>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());


        }
    }
}
