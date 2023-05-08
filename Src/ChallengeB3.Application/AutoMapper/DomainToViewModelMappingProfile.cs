using AutoMapper;
using ChallengeB3.Application.ViewModel;
using ChallengeB3.Domain.Models;

namespace ChallengeB3.Application.AutoMapper;

public  class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile() 
    {
        CreateMap<Register, RegisterViewModel>();
    }
}
