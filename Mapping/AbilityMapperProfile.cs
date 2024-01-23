using AutoMapper;
using PokemonAPI.Models;

namespace PokemonAPI.Mapping;

public class AbilityMapperProfile : Profile
{
    public AbilityMapperProfile()
    {
        CreateMap<AbilityDao, Ability>()
            .ForMember(dm => dm.Name, opt => opt.MapFrom(src => src.Name));
    }
}