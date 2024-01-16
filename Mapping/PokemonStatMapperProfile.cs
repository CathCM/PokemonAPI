using AutoMapper;
using PokemonAPI.Models;

namespace PokemonAPI.Mapping;

public class PokemonStatMapperProfile: Profile
{
    public PokemonStatMapperProfile()
    {
        CreateMap<PokemonDao, PokemonStat>()
            .ForMember(dm=>dm.Name,opt=>opt.MapFrom(src=>"Hp"))
            .ForMember(dm => dm.BaseStat, opt => opt.MapFrom(src => src.Hp));
        CreateMap<PokemonDao, PokemonStat>()
            .ForMember(dm=>dm.Name,opt=>opt.MapFrom(src=>"Defense"))
            .ForMember(dm => dm.BaseStat, opt => opt.MapFrom(src => src.Defense));
        CreateMap<PokemonDao, PokemonStat>()
            .ForMember(dm=>dm.Name,opt=>opt.MapFrom(src=>"Attack"))
            .ForMember(dm => dm.BaseStat, opt => opt.MapFrom(src => src.Attack));
        CreateMap<PokemonDao, PokemonStat>()
            .ForMember(dm=>dm.Name,opt=>opt.MapFrom(src=>"SpecialAttack"))
            .ForMember(dm => dm.BaseStat, opt => opt.MapFrom(src => src.SpecialAttack));
        CreateMap<PokemonDao, PokemonStat>()
            .ForMember(dm=>dm.Name,opt=>opt.MapFrom(src=>"Hp"))
            .ForMember(dm => dm.BaseStat, opt => opt.MapFrom(src => src.Hp));
    }
}