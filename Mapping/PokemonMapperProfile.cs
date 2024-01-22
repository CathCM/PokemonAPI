using AutoMapper;
using PokemonAPI.Models;

namespace PokemonAPI.Mapping;

public class PokemonMapperProfile : Profile
{
    public PokemonMapperProfile()
    {
        CreateMap<PokemonDao, Pokemon>()
            .ForMember(dm => dm.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dm => dm.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dm => dm.Abilities, opt => opt.MapFrom(src => src.PokemonAbility.Select(e => new PokemonAbility
            {
                Name = e.AbilityName,
                IsHidden = e.IsHidden
            }).ToList()))
            .ForMember(dm => dm.Stats, opt => opt.MapFrom(src => new List<PokemonStat>
            {
                new PokemonStat() { Name = "Hp", BaseStat = src.Hp },
                new PokemonStat() { Name = "Defense", BaseStat = src.Defense },
                new PokemonStat() { Name = "Attack", BaseStat = src.Attack },
                new PokemonStat() { Name = "SpecialAttack", BaseStat = src.SpecialAttack },
                new PokemonStat() { Name = "SpecialDefense", BaseStat = src.SpecialDefense },
                new PokemonStat() { Name = "Speed", BaseStat = src.Speed },
            }))
            .ForMember(dm => dm.Types, opt => opt.MapFrom(src => src.Types.Select(e => new Types
            {
                Name = e.Name
            }).ToList()));
    }
}