using AutoMapper;
using HorseBets.Bets.Models;
using HorseBets.Bets.Models.Dto;

namespace HorseBets
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Match, MatchDto>()
            .ForMember(dest => dest.WinnerName, opt => opt.MapFrom(src => src.Winner != null ? src.Winner.Horse.Name : "None"));
            CreateMap<MatchDto, Match>();
            CreateMap<Horse, HorseDto>();
            CreateMap<HorseDto, Horse>();
            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();
            CreateMap<Bet, BetDto>();
            CreateMap<BetDto, Bet>();
        }
    }
}