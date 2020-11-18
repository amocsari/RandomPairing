using System;
using System.Collections.Generic;

namespace RandomPairer.Bll.Extensions
{
    public static class IEnumerableExtensions
    {
        public static TElement GetRandomElement<TElement>(this List<TElement> enumerable) where TElement : class
        {
            if (enumerable.Count == 0)
                return null;

            var random = new Random();
            return enumerable[random.Next(enumerable.Count)];
        }
    }
}
