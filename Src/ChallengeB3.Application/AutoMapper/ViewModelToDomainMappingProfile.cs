﻿using AutoMapper;
using ChallengeB3.Application.ViewModel;
using ChallengeB3.Domain.Commands;

namespace ChallengeB3.Application.AutoMapper;

public class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        CreateMap<RegisterViewModel, InsertRegisterCommand>()
                .ConstructUsing(c => new InsertRegisterCommand(c.Description, c.Status, c.Date));
        
        CreateMap<RegisterViewModel, UpdateRegisterCommand>()
            .ConstructUsing(c => new UpdateRegisterCommand(c.RegisterId, c.Description, c.Status, c.Date));


    }
}
