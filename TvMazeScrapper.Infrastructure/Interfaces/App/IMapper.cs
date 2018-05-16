using System.Collections.Generic;

namespace TvMazeScrapper.Infrastructure.Interfaces.App
{
    public interface IMapper
    {
        TTarget Map<TTarget>(object source) where TTarget : class;

        IEnumerable<TTarget> MapCollection<TSource, TTarget>(IEnumerable<TSource> source)
            where TTarget : class
            where TSource : class;
    }
}
