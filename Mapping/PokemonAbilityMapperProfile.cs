using AutoMapper;
using PokemonAPI.Models;

namespace PokemonAPI.Mapping;

public class PokemonAbilityMapperProfile: Profile
{
    public PokemonAbilityMapperProfile()
    {
        CreateMap<PokemonAbilityDao, PokemonAbility>()
            .ForMember(dm => dm.Name, opt => opt.MapFrom(src => src.AbilityName))
            .ForMember(dm => dm.IsHidden, opt => opt.MapFrom(src => src.IsHidden));
    }
}