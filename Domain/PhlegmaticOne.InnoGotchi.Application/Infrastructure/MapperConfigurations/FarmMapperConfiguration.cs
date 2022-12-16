﻿using AutoMapper;
using PhlegmaticOne.InnoGotchi.Domain.Models;
using PhlegmaticOne.InnoGotchi.Shared.Farms;

namespace PhlegmaticOne.InnoGotchi.Application.Infrastructure.MapperConfigurations;

public class FarmMapperConfiguration : Profile
{
    public FarmMapperConfiguration()
    {
        CreateMap<Farm, DetailedFarmDto>()
            .ForMember(x => x.InnoGotchies, o => o.MapFrom(y => y.InnoGotchies.ToList()))
            .ForMember(x => x.Email, o => o.MapFrom(x => x.Owner.User.Email))
            .ForMember(x => x.FirstName, o => o.MapFrom(x => x.Owner.FirstName))
            .ForMember(x => x.LastName, o => o.MapFrom(x => x.Owner.LastName));
    }
}