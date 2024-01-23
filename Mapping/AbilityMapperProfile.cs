using AutoMapper;
using PokemonAPI.Models;

namespace PokemonAPI.Mapping;

public class AbilityMapperProfile : Profile
{
    public AbilityMapperProfile()
    {
        CreateMap<AbilityDao, Ability>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Ability, AbilityDao>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}