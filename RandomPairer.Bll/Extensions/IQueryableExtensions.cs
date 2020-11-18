using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace RandomPairer.Bll.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ProjectTo<T>(this IQueryable source, IMapper mapper) =>
            source.ProjectTo<T>(mapper.ConfigurationProvider);
    }
}
