using AutoMapper;
using RandomPairer.Transfer.Pairing;

namespace RandomPairer.Bll.Mappings
{
    public static partial class Mappings
    {
        public static IMapperConfigurationExpression ConfigurePairing(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Dal.Entities.User, PairingDto>()
                .ForMember(dest => dest.Who, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Whom, opt => opt.MapFrom(src => src.Pair.Name))
                .ForMember(dest => dest.Secret, opt => opt.MapFrom(src => src.Secret));

            return cfg;
        }
    }
}
