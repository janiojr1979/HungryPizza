using System;
using System.Collections.Generic;

namespace HungryPizza.Infra.CrossCutting.Tools
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var item in values)
            {
                action(item);
            }
        }
    }
}
