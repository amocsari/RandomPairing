using AutoMapper;

namespace RandomPairer.Bll.Mappings
{
    public static class MapperConfig
    {
        public static IMapper ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ConfigurePairing();
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}
