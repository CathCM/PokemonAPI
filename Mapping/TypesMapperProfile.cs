using AutoMapper;
using PokemonAPI.Models;

namespace PokemonAPI.Mapping;

public class TypesMapperProfile: Profile
{
    public TypesMapperProfile()
    {
        CreateMap<TypeDao, TypeDao>()
            .ForMember(dm => dm.Name, opt => opt.MapFrom(src => src.Name));
    }
}