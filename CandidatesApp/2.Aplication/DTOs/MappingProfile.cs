using AutoMapper;
using CandidatesApp._3.Infrastructure.Commands;
using CandidatesApp.Models;

namespace CandidatesApp._2.Aplication.DTOs
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
            
        }
    }
}
