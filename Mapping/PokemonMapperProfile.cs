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
        
        CreateMap<Pokemon, PokemonDao>()
            .ForMember(dest => dest.PokemonAbility, opt => opt.MapFrom(src => src.Abilities.Select(e => new PokemonAbilityDao
            {
                AbilityName = e.Name,
                IsHidden = e.IsHidden
            }).ToList()))
            .ForMember(dest => dest.Hp, opt => opt.MapFrom(src => src.Stats.FirstOrDefault(s => s.Name == "Hp").BaseStat))
            .ForMember(dest => dest.Defense, opt => opt.MapFrom(src => src.Stats.FirstOrDefault(s => s.Name == "Defense").BaseStat))
            .ForMember(dest => dest.Attack, opt => opt.MapFrom(src => src.Stats.FirstOrDefault(s => s.Name == "Attack").BaseStat))
            .ForMember(dest => dest.SpecialAttack, opt => opt.MapFrom(src => src.Stats.FirstOrDefault(s => s.Name == "SpecialAttack").BaseStat))
            .ForMember(dest => dest.SpecialDefense, opt => opt.MapFrom(src => src.Stats.FirstOrDefault(s => s.Name == "SpecialDefense").BaseStat))
            .ForMember(dest => dest.Speed, opt => opt.MapFrom(src => src.Stats.FirstOrDefault(s => s.Name == "Speed").BaseStat))
            .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types.Select(t => new TypeDao
            {
                Name = t.Name
            }).ToList()));

    }
}